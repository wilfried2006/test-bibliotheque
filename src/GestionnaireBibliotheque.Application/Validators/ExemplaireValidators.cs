using FluentValidation;
using GestionnaireBibliotheque.Application.DTOs.Requests;

namespace GestionnaireBibliotheque.Application.Validators;

public class CreateExemplaireRequestValidator : AbstractValidator<CreateExemplaireRequest>
{
    public CreateExemplaireRequestValidator()
    {
        RuleFor(x => x.OuvrageId).GreaterThan(0);
        RuleFor(x => x.EtatDisponibilite).IsInEnum();
    }
}
