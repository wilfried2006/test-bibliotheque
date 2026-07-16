using GestionnaireBibliotheque.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionnaireBibliotheque.API.Middleware;

/// <summary>
/// Traduit les exceptions en réponses ProblemDetails :
/// - EmpruntDejaRetourneException / DbUpdateConcurrencyException → 409 Conflict
/// - DomainException (autre règle métier) → 422 Unprocessable Entity
/// - annulation client → 499 (pas de corps)
/// - toute autre exception → 500 (détail générique, message réel journalisé côté serveur)
/// </summary>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            // Annulation à l'initiative du client : ce n'est pas une erreur serveur.
            logger.LogInformation("Requête annulée par le client : {Path}", context.Request.Path);
            if (!context.Response.HasStarted)
                context.Response.StatusCode = 499; // Client Closed Request
        }
        catch (RessourceIntrouvableException ex)
        {
            logger.LogWarning(ex, "Ressource introuvable lors du traitement de {Path}", context.Request.Path);
            await WriteProblemAsync(context, StatusCodes.Status404NotFound, "Ressource introuvable.", ex.Message);
        }
        catch (ConflitMetierException ex)
        {
            logger.LogWarning(ex, "Conflit métier lors du traitement de {Path}", context.Request.Path);
            await WriteProblemAsync(context, StatusCodes.Status409Conflict, "Conflit métier.", ex.Message);
        }
        catch (DomainException ex)
        {
            logger.LogWarning(ex, "Règle métier non respectée lors du traitement de {Path}", context.Request.Path);
            await WriteProblemAsync(context, StatusCodes.Status422UnprocessableEntity, "Règle métier non respectée.", ex.Message);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogWarning(ex, "Conflit de concurrence lors du traitement de {Path}", context.Request.Path);
            await WriteProblemAsync(
                context,
                StatusCodes.Status409Conflict,
                "Conflit de concurrence.",
                "La ressource a été modifiée par une autre opération. Veuillez réessayer.");
        }
        catch (Exception ex)
        {
            // Le message réel n'est journalisé que côté serveur (pas exposé au client).
            logger.LogError(ex, "Erreur non gérée lors du traitement de {Path}", context.Request.Path);
            await WriteProblemAsync(
                context,
                StatusCodes.Status500InternalServerError,
                "Une erreur inattendue est survenue.",
                "Une erreur interne est survenue. Contactez l'administrateur si le problème persiste.");
        }
    }

    private static async Task WriteProblemAsync(HttpContext context, int statusCode, string title, string detail)
    {
        if (context.Response.HasStarted)
            return;

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };
        problem.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problem);
    }
}
