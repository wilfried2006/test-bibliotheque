using AutoMapper;
using GestionnaireBibliotheque.Application.DTOs.Responses;
using GestionnaireBibliotheque.Domain.Entities;

namespace GestionnaireBibliotheque.Application.Mappings;

/// <summary>Mappages Entité → DTO (lecture). L'écriture passe par les fabriques du domaine.</summary>
public class BibliothequeProfile : Profile
{
    public BibliothequeProfile()
    {
        CreateMap<Auteur, AuteurResponse>();
        CreateMap<Ouvrage, OuvrageResponse>();
        CreateMap<Exemplaire, ExemplaireResponse>();
        CreateMap<Emprunt, EmpruntResponse>();
        CreateMap<TypeAdherent, TypeAdherentResponse>();

        CreateMap<Membre, MembreResponse>()
            .ForCtorParam(nameof(MembreResponse.Email), opt => opt.MapFrom(s => s.Email == null ? null : s.Email.Valeur));

        CreateMap<Penalite, PenaliteResponse>()
            .ForCtorParam(nameof(PenaliteResponse.Montant), opt => opt.MapFrom(s => s.Montant.Valeur));
    }
}
