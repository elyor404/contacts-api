namespace Contacts.Api.Dtos;

public class ContactDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? Address { get; set; }
}
