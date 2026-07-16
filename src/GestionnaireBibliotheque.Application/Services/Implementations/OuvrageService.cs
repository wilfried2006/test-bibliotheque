using AutoMapper;
using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;
using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Interfaces;

namespace GestionnaireBibliotheque.Application.Services;

public class OuvrageService(
    IOuvrageRepository ouvrageRepository,
    IExemplaireRepository exemplaireRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IOuvrageService
{
    public async Task<IReadOnlyList<OuvrageResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        => mapper.Map<IReadOnlyList<OuvrageResponse>>(await ouvrageRepository.ListAsync(cancellationToken));

    public async Task<OuvrageResponse> CreateAsync(CreateOuvrageRequest dto, CancellationToken cancellationToken = default)
    {
        var ouvrage = Ouvrage.Creer(dto.Titre, dto.AuteurId);
        await ouvrageRepository.AddAsync(ouvrage, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken); // obtient l'Id de l'ouvrage

        for (var i = 0; i < dto.NombreExemplaires; i++)
            await exemplaireRepository.AddAsync(ouvrage.CreerExemplaire(), cancellationToken);

        if (dto.NombreExemplaires > 0)
            await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<OuvrageResponse>(ouvrage);
    }
}
