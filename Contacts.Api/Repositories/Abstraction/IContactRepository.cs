namespace Contacts.Api.Repostories.Abstraction;
using Contacts.Api.Entities;

public interface IContactRepository
{
    ValueTask<Contact> InsertAsync(Contact contact, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken = default);

    ValueTask<Contact> GetSingleAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<Contact> UpdateAsync(Contact contact, CancellationToken cancellationToken = default);
    ValueTask<Contact> UpdateSinglePartAsync(Contact contact, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(int id, CancellationToken cancellationToken = default);
    ValueTask<bool> IsPhoneExistsAsync(string PhoneNumber, CancellationToken cancellationToken = default);
    ValueTask<bool> IsEmailExistsAsync(string Email, CancellationToken cancellationToken = default);
    ValueTask<bool> IsIdExistsAsync(int id, CancellationToken cancellationToken = default);
}