using AutoMapper;
using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;
using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Interfaces;

namespace GestionnaireBibliotheque.Application.Services;

public class ExemplaireService(IExemplaireRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IExemplaireService
{
    public async Task<IReadOnlyList<ExemplaireResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        => mapper.Map<IReadOnlyList<ExemplaireResponse>>(await repository.ListAsync(cancellationToken));

    public async Task<ExemplaireResponse> CreateAsync(CreateExemplaireRequest dto, CancellationToken cancellationToken = default)
    {
        var exemplaire = Exemplaire.Creer(dto.OuvrageId, dto.EtatDisponibilite);
        await repository.AddAsync(exemplaire, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<ExemplaireResponse>(exemplaire);
    }
}
