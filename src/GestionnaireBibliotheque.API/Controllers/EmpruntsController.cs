using GestionnaireBibliotheque.Application.Services;
using Microsoft.AspNetCore.Mvc;
using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpruntsController(IEmpruntService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmpruntResponse>>> GetAll(CancellationToken cancellationToken)
        => Ok(await service.GetAllAsync(cancellationToken));

    [HttpPost]
    public async Task<ActionResult<EmpruntResponse>> Create(CreateEmpruntRequest dto, CancellationToken cancellationToken)
    {
        var created = await service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetAll), null, created);
    }

    /// <summary>Retourne un ouvrage emprunté. Constate un retard si le retour est tardif.</summary>
    [HttpPost("{id:int}/retour")]
    public async Task<ActionResult<RetourEmpruntResponse>> Retourner(
        int id,
        [FromBody] RetourEmpruntRequest? request,
        CancellationToken cancellationToken)
    {
        var result = await service.RetournerAsync(id, request?.DateRetour, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
