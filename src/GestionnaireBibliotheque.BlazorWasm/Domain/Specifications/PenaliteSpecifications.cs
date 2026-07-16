using GestionnaireBibliotheque.BlazorWasm.Models;

namespace GestionnaireBibliotheque.BlazorWasm.Domain.Specifications;

/// <summary>Pénalités non payées d'un membre.</summary>
public class PenalitesNonPayeesSpecification : ISpecification<PenaliteDto>
{
    private readonly int _membreId;

    public PenalitesNonPayeesSpecification(int membreId)
    {
        _membreId = membreId;
    }

    public Func<PenaliteDto, bool> Criteria => p => p.MembreId == _membreId;
    public string Description => $"Pénalités du membre {_membreId}";
}

/// <summary>Pénalités importantes (montant > 20€).</summary>
public class PenalitesImportantesSpecification : ISpecification<PenaliteDto>
{
    public Func<PenaliteDto, bool> Criteria => p => p.Montant > 20;
    public string Description => "Pénalités importantes (> 20€)";
}

/// <summary>Pénalités récentes (des 30 derniers jours).</summary>
public class PenalitesRecentesSpecification : ISpecification<PenaliteDto>
{
    public Func<PenaliteDto, bool> Criteria => p =>
        p.DatePenalite >= DateTime.Today.AddDays(-30);
    public string Description => "Pénalités des 30 derniers jours";
}

/// <summary>Combine plusieurs specifications : pénalités importantes et non payées d'un membre.</summary>
public class PenalitesImportantesMembreSpecification : ISpecification<PenaliteDto>
{
    private readonly int _membreId;

    public PenalitesImportantesMembreSpecification(int membreId)
    {
        _membreId = membreId;
    }

    public Func<PenaliteDto, bool> Criteria => p =>
        p.MembreId == _membreId && p.Montant > 20;

    public string Description => $"Pénalités importantes du membre {_membreId}";
}
