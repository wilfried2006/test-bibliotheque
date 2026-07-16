using AutoMapper;
using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;
using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Interfaces;

namespace GestionnaireBibliotheque.Application.Services;

public class AuteurService(IAuteurRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IAuteurService
{
    public async Task<IReadOnlyList<AuteurResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        => mapper.Map<IReadOnlyList<AuteurResponse>>(await repository.ListAsync(cancellationToken));

    public async Task<AuteurResponse> CreateAsync(CreateAuteurRequest dto, CancellationToken cancellationToken = default)
    {
        var auteur = Auteur.Creer(dto.Nom, dto.Prenom);
        await repository.AddAsync(auteur, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<AuteurResponse>(auteur);
    }
}
