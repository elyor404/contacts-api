using Contact.Api.Dtos;
using Contact.Api.Model;



namespace Contact.Api.Abstraction;

public interface IContactService
{
    ValueTask<Model.Contact> CreateContactAsync(CreateContact dto, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Model.Contact>> GetAllContactsAsync(CancellationToken cancellationToken = default);
    ValueTask<Model.Contact> GetSingleContactAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<Model.Contact> UpdateContactAsync(int id, UpdateContact contact, CancellationToken cancellationToken = default);
    ValueTask<Model.Contact> UpdateSinglePartOfContactAsync(int id, PatchContact patchContact, CancellationToken cancellationToken = default);
    ValueTask DeleteContactAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<bool> IsPhoneExistsAsync(string PhoneNumber, CancellationToken cancellationToken = default);
    ValueTask<bool> IsEmailExistsAsync(string Email, CancellationToken cancellationToken = default);
    ValueTask<bool> IsIdExistsAsync(int id, CancellationToken cancellationToken = default);

}