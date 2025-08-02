using Contact.Api.Abstraction;
using Contact.Api.Dtos;
using FluentValidation;

namespace Contact.Api.Validators;

public class PatchContactValidator : AbstractValidator<PatchContactDto>
{
    public PatchContactValidator(IContactService service)
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(@"^\+998\d{9}$")
            .MustAsync(async (phoneNumber, token) => await service.IsPhoneExistsAsync(phoneNumber, token) is false)
            .WithMessage("Contact phone number must be unique");
    }
}