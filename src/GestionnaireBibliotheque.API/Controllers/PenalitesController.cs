using GestionnaireBibliotheque.Application.Services;
using Microsoft.AspNetCore.Mvc;
using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PenalitesController(IPenaliteService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PenaliteResponse>>> GetAll(CancellationToken cancellationToken)
        => Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PenaliteResponse>> GetById(int id, CancellationToken cancellationToken)
        => Ok(await service.GetByIdAsync(id, cancellationToken));

    [HttpPost("{id:int}/payer")]
    public async Task<IActionResult> MarquerPaye(int id, CancellationToken cancellationToken)
        => await service.MarquerPayeAsync(id, cancellationToken) ? NoContent() : NotFound();
}
