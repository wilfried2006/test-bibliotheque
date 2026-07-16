using FluentValidation;
using GestionnaireBibliotheque.Application.DTOs.Requests;

namespace GestionnaireBibliotheque.Application.Validators;

public class CreateAuteurRequestValidator : AbstractValidator<CreateAuteurRequest>
{
    public CreateAuteurRequestValidator()
    {
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Prenom).NotEmpty().MaximumLength(200);
    }
}
