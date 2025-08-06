using AutoMapper;
using Contacts.Api.Dtos;
using Contacts.Api.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contacts.Api.Services;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<CreateContactDto, CreateContact>();
        CreateMap<UpdateContactDto, UpdateContact>();
        CreateMap<ContactDto, Model.Contact>();

        CreateMap<CreateContact, Model.Contact>();
        CreateMap<Model.Contact, ContactDto>();
        CreateMap<UpdateContact, Model.Contact>();
        CreateMap<PatchContactDto, PatchContact>();
        CreateMap<PatchContact, Model.Contact>();

        CreateMap<CreateContact, Entities.Contact>();
        CreateMap<Entities.Contact, Model.Contact>();
        CreateMap<UpdateContact, Entities.Contact>();
        CreateMap<PatchContact, Entities.Contact>();
        CreateMap<Entities.Contact, UpdateContact>();
    }
}