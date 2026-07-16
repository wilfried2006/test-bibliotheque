using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.Application.Services;

public interface IEmpruntService
{
    Task<IReadOnlyList<EmpruntResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EmpruntResponse> CreateAsync(CreateEmpruntRequest dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retourne un ouvrage emprunté : clôture l'emprunt (avec constat de retard le cas
    /// échéant) et libère l'exemplaire.
    /// </summary>
    /// <returns>Le résultat du retour, ou <c>null</c> si l'emprunt est introuvable.</returns>
    /// <exception cref="GestionnaireBibliotheque.Domain.Exceptions.EmpruntDejaRetourneException">
    /// Si l'emprunt a déjà été retourné.</exception>
    Task<RetourEmpruntResponse?> RetournerAsync(
        int empruntId,
        DateTime? dateRetour = null,
        CancellationToken cancellationToken = default);
}
