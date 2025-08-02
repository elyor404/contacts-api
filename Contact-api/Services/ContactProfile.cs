using AutoMapper;
using Contact.Api.Dtos;
using Contact.Api.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contact.Api.Services;

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
    }
}