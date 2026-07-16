using GestionnaireBibliotheque.Application.Services;
using Microsoft.AspNetCore.Mvc;
using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExemplairesController(IExemplaireService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ExemplaireResponse>>> GetAll(CancellationToken cancellationToken)
        => Ok(await service.GetAllAsync(cancellationToken));

    [HttpPost]
    public async Task<ActionResult<ExemplaireResponse>> Create(CreateExemplaireRequest dto, CancellationToken cancellationToken)
    {
        var created = await service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetAll), null, created);
    }
}
