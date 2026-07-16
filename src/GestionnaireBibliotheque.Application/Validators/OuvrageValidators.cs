using FluentValidation;
using GestionnaireBibliotheque.Application.DTOs.Requests;

namespace GestionnaireBibliotheque.Application.Validators;

public class CreateOuvrageRequestValidator : AbstractValidator<CreateOuvrageRequest>
{
    public CreateOuvrageRequestValidator()
    {
        RuleFor(x => x.Titre).NotEmpty().MaximumLength(300);
        RuleFor(x => x.AuteurId).GreaterThan(0);
        RuleFor(x => x.NombreExemplaires).InclusiveBetween(0, 1000);
    }
}
