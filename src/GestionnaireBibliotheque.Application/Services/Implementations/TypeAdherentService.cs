using AutoMapper;
using GestionnaireBibliotheque.Application.DTOs.Responses;
using GestionnaireBibliotheque.Domain.Interfaces;

namespace GestionnaireBibliotheque.Application.Services;

public class TypeAdherentService(ITypeAdherentRepository repository, IMapper mapper) : ITypeAdherentService
{
    public async Task<IReadOnlyList<TypeAdherentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        => mapper.Map<IReadOnlyList<TypeAdherentResponse>>(await repository.ListAsync(cancellationToken));
}
