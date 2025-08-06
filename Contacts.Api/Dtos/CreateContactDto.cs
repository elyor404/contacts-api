namespace Contacts.Api.Dtos;

public class CreateContactDto
{

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Address { get; set; }
}
