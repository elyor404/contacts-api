
namespace Contacts.Api.Model;

public class CreateContact
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Address { get; set; }
}