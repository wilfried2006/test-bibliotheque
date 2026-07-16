using FluentValidation;
using GestionnaireBibliotheque.Application.DTOs.Requests;

namespace GestionnaireBibliotheque.Application.Validators;

public class CreateEmpruntRequestValidator : AbstractValidator<CreateEmpruntRequest>
{
    public CreateEmpruntRequestValidator()
    {
        RuleFor(x => x.OuvrageId).GreaterThan(0);
        RuleFor(x => x.MembreId).GreaterThan(0);
    }
}
