using AutoMapper;
using Contacts.Api.Abstraction;
using Contacts.Api.Model;
using Contacts.Api.Repostories.Abstraction;


namespace Contacts.Api.Services;

public class ContactService(
        IContactRepository repository,
        IMapper mapper) : IContactService
{

    public async ValueTask<Model.Contact> CreateContactAsync(CreateContact createdContact, CancellationToken cancellationToken = default)
    {
        var contact = await repository.InsertAsync(mapper.Map<Entities.Contact>(createdContact), cancellationToken);
        return mapper.Map<Model.Contact>(contact);
    }
    public async ValueTask<IEnumerable<Model.Contact>> GetAllContactsAsync(CancellationToken cancellationToken = default)
    {
        var contacts = await repository.GetAllAsync(cancellationToken);
        return contacts.Select(mapper.Map<Model.Contact>);
    }

    public async ValueTask<Model.Contact> GetSingleContactAsync(int id, CancellationToken cancellationToken = default)
    {
        var contact = await repository.GetSingleAsync(id, cancellationToken);
        return mapper.Map<Model.Contact>(contact);
    }

    public async ValueTask<Model.Contact> UpdateContactAsync(int id, UpdateContact contact, CancellationToken cancellationToken = default)
    {
        var found = await repository.GetSingleAsync(id, cancellationToken);
        found.UpdatedAt = DateTimeOffset.UtcNow;
        mapper.Map(contact,found);
        return mapper.Map<Model.Contact>(await repository.UpdateAsync(found, cancellationToken));
    }

    public async ValueTask<Model.Contact> UpdateSinglePartOfContactAsync(int id, PatchContact patchContact, CancellationToken cancellationToken = default)
    {
        var found = await repository.GetSingleAsync(id, cancellationToken);
        found.UpdatedAt = DateTimeOffset.UtcNow;
        mapper.Map(patchContact,found);
        return mapper.Map<Model.Contact>(await repository.UpdateSinglePartAsync(found, cancellationToken));
    }
    public async ValueTask DeleteContactAsync(int id, CancellationToken cancellationToken = default)
        => await repository.DeleteAsync(id, cancellationToken);
    public async ValueTask<bool> IsEmailExistsAsync(string Email, CancellationToken cancellationToken = default)
        => await repository.IsEmailExistsAsync(Email, cancellationToken);

    public async ValueTask<bool> IsPhoneExistsAsync(string PhoneNumber, CancellationToken cancellationToken = default)
        => await repository.IsPhoneExistsAsync(PhoneNumber, cancellationToken);
    public async ValueTask<bool> IsIdExistsAsync(int id, CancellationToken cancellationToken = default)
        => await repository.IsIdExistsAsync(id, cancellationToken);
}