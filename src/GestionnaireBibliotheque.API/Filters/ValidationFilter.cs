using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestionnaireBibliotheque.API.Filters;

/// <summary>
/// Filtre global : pour chaque argument d'action ayant un validateur FluentValidation
/// enregistré, valide et renvoie 400 (ValidationProblemDetails) si invalide.
/// </summary>
public class ValidationFilter(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            if (serviceProvider.GetService(validatorType) is not IValidator validator)
                continue;

            var validationContext = new ValidationContext<object>(argument);
            var result = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);

            if (result.IsValid)
                continue;

            foreach (var error in result.Errors)
                context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState));
            return;
        }

        await next();
    }
}
