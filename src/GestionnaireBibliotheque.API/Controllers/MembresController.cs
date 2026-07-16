using GestionnaireBibliotheque.Application.Services;
using Microsoft.AspNetCore.Mvc;
using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembresController(IMembreService service, IPenaliteService penaliteService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MembreResponse>>> GetAll(CancellationToken cancellationToken)
        => Ok(await service.GetAllAsync(cancellationToken));

    [HttpPost]
    public async Task<ActionResult<MembreResponse>> Create(CreateMembreRequest dto, CancellationToken cancellationToken)
    {
        var created = await service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetAll), null, created);
    }

    /// <summary>Liste les pénalités d'un membre.</summary>
    [HttpGet("{id:int}/penalites")]
    public async Task<ActionResult<IReadOnlyList<PenaliteResponse>>> GetPenalites(int id, CancellationToken cancellationToken)
        => Ok(await penaliteService.GetByMembreAsync(id, cancellationToken));

    /// <summary>Total des pénalités en cours d'un membre (plafonné à 10 €).</summary>
    [HttpGet("{id:int}/penalites/total")]
    public async Task<ActionResult<TotalPenalitesResponse>> GetTotalPenalites(int id, CancellationToken cancellationToken)
        => Ok(await penaliteService.GetTotalEnCoursAsync(id, cancellationToken));
}
