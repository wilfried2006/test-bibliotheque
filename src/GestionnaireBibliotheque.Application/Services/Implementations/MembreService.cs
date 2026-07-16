using AutoMapper;
using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;
using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Interfaces;

namespace GestionnaireBibliotheque.Application.Services;

public class MembreService(IMembreRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IMembreService
{
    public async Task<IReadOnlyList<MembreResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        => mapper.Map<IReadOnlyList<MembreResponse>>(await repository.ListAsync(cancellationToken));

    public async Task<MembreResponse> CreateAsync(CreateMembreRequest dto, CancellationToken cancellationToken = default)
    {
        var membre = Membre.Creer(dto.Nom, dto.Prenom, dto.Email, dto.TypeAdherentId);
        await repository.AddAsync(membre, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<MembreResponse>(membre);
    }
}
