using GestionnaireBibliotheque.BlazorWasm.Domain.Emprunts;
using GestionnaireBibliotheque.BlazorWasm.Models;

namespace GestionnaireBibliotheque.BlazorWasm.Domain.Specifications;

/// <summary>Emprunts actuellement actifs (non retournés).</summary>
public class EmpruntActifSpecification : ISpecification<EmpruntDto>
{
    public Func<EmpruntDto, bool> Criteria => e => e.DateRetourReel == null && e.EtatEmprunt == "Actif";
    public string Description => "Emprunts actuellement actifs";
}

/// <summary>Emprunts en retard (date de retour dépassée sans retour).</summary>
public class EmpruntEnRetardSpecification : ISpecification<EmpruntDto>
{
    public Func<EmpruntDto, bool> Criteria => e =>
        e.DateRetourReel == null && e.DateRetourPrevue < DateTime.Today;
    public string Description => "Emprunts en retard";
}

/// <summary>Emprunts d'un membre spécifique.</summary>
public class EmpruntParMembreSpecification : ISpecification<EmpruntDto>
{
    private readonly int _membreId;

    public EmpruntParMembreSpecification(int membreId)
    {
        _membreId = membreId;
    }

    public Func<EmpruntDto, bool> Criteria => e => e.MembreId == _membreId;
    public string Description => $"Emprunts du membre {_membreId}";
}

/// <summary>Combine plusieurs specifications avec AND.</summary>
public class EmpruntEnRetardParMembreSpecification : ISpecification<EmpruntDto>
{
    private readonly int _membreId;

    public EmpruntEnRetardParMembreSpecification(int membreId)
    {
        _membreId = membreId;
    }

    public Func<EmpruntDto, bool> Criteria => e =>
        e.MembreId == _membreId &&
        e.DateRetourReel == null &&
        e.DateRetourPrevue < DateTime.Today;

    public string Description => $"Emprunts en retard du membre {_membreId}";
}
