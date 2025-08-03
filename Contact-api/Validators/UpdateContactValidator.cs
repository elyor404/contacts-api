using System.Data;
using Contact.Api.Abstraction;
using Contact.Api.Dtos;
using FluentValidation;

namespace Contact.Api.Validators;

public class UpdateContactValidator : AbstractValidator<UpdateContactDto>
{
    public UpdateContactValidator(IContactService service)
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
      
        RuleFor(c => c.Id)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (id, token) => await service.IsIdExistsAsync(id, token))
            .WithMessage((dto, id) => $"Contact with id '{id}' doesn't exist.");

        RuleFor(c => c.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MinimumLength(2)
            .MaximumLength(50)
            .Matches("^[a-zA-Z\\-\\s]+$")
            .WithMessage("First name can only contain letters, hyphens, and spaces.");

        RuleFor(c => c.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MinimumLength(2)
            .MaximumLength(50)
            .Matches("^[a-zA-Z\\-\\s]+$")
            .WithMessage("Last name can only contain letters, hyphens, and spaces.");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(@"^\+998\d{9}$")
            .MustAsync(async (phoneNumber, token) => await service.IsPhoneExistsAsync(phoneNumber, token) is false)
            .WithMessage("Contact phone number must be unique");
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Email is required.")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Invalid email format.")
            .MustAsync(async (email, token) => await service.IsEmailExistsAsync(email, token) is false)
            .WithMessage("Email must be unique");

        RuleFor(c => c.StartsAt)
            .NotEmpty()
            .WithMessage("'StartsAt' is required")
            .When(q => q.EndsAt is not null)
            .WithMessage("{PropertyName} is required if EndsAt is provided.");

    }
}