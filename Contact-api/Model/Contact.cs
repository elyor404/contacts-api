namespace Contact.Api.Model;

public record Contact
(

    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber,
    DateTimeOffset? StartsAt,
    DateTimeOffset? EndsAt,
    string? Address
)
{
    public int Id { get; set; }
}