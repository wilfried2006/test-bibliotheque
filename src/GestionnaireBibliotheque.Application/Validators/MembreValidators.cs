using FluentValidation;
using GestionnaireBibliotheque.Application.DTOs.Requests;

namespace GestionnaireBibliotheque.Application.Validators;

public class CreateMembreRequestValidator : AbstractValidator<CreateMembreRequest>
{
    public CreateMembreRequestValidator()
    {
        RuleFor(x => x.Nom).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Prenom).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(256)
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
        RuleFor(x => x.TypeAdherentId).GreaterThan((byte)0);
    }
}
