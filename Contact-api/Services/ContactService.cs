using System.Windows.Markup;
using AutoMapper;
using Contact.Api.Abstraction;
using Contact.Api.Dtos;
using Contact.Api.Exceptions;
using Contact.Api.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Contact.Api.Services;

public class ContactService(IMapper mapper) : IContactService
{
    private Dictionary<string, Model.Contact> contacts = [];
    private int IdIndex = 1;
    public ValueTask<Model.Contact> CreateContactAsync(CreateContact createdContact, CancellationToken cancellationToken = default)
    {
        if (contacts.Values.Any(c => c.PhoneNumber == createdContact.PhoneNumber))
        {
            throw new CustomConflictException($"Contact with phone number {createdContact.PhoneNumber} already exists");
        }
        var newContact = mapper.Map<Model.Contact>(createdContact);
        newContact.Id= IdIndex++;
        contacts.Add(createdContact.PhoneNumber, newContact);
        return ValueTask.FromResult(newContact);
    }
    public ValueTask<IEnumerable<Model.Contact>> GetAllContactsAsync(CancellationToken cancellationToken = default)
        => new (contacts.Values);

    public ValueTask<Model.Contact> GetSingleContactAsync(int id, CancellationToken cancellationToken = default)
    {

        var contact = contacts.Values.FirstOrDefault(c => c.Id == id) ?? throw new CustomNotFoundException($"Contact with id {id} was not found.");
        return ValueTask.FromResult(contact);
    }
    public async ValueTask<Model.Contact> UpdateContactAsync(int id, UpdateContact contact, CancellationToken cancellationToken = default)
    {
        var contactToUpdate = await GetSingleContactAsync(id, cancellationToken);
        contacts.Remove(contactToUpdate.PhoneNumber!);
        mapper.Map(contact, contactToUpdate);
        contacts[contactToUpdate.PhoneNumber!] = contactToUpdate;
        return contactToUpdate;
    }

    // public async ValueTask<Model.Contact> UpdateContactAsync(int id, UpdateContact contact, CancellationToken cancellationToken = default)
    // {
    //     var result = await GetSingleContactAsync(id, cancellationToken);
    //     mapper.Map(contact, result);
    //     return result;
    // }

    public async ValueTask<Model.Contact> UpdateSinglePartOfContactAsync(int id, PatchContact patchContact, CancellationToken cancellationToken = default)
    {
        var result = await GetSingleContactAsync(id, cancellationToken);
        mapper.Map(patchContact, result);
        return result;
    }
    public async ValueTask DeleteContactAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await GetSingleContactAsync(id, cancellationToken);
        contacts.Remove(result.PhoneNumber!);
    }
    public ValueTask<bool> IsEmailExistsAsync(string Email, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(contacts.Values.Any(a => a.Email==Email));

    public ValueTask<bool> IsPhoneExistsAsync(string PhoneNumber, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(contacts.Values.Any(a => a.PhoneNumber==PhoneNumber));


}