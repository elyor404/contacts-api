namespace Contact.Api.Model;

public record CreateContact
(
    string? FirstName ,
    string? LastName,
    string Email,
    string PhoneNumber,
    DateTimeOffset? StartsAt,
    DateTimeOffset? EndsAt ,
    string? Address
);