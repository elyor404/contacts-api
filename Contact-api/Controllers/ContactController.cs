using AutoMapper;
using Contact.Api.Abstraction;
using Contact.Api.Dtos;
using Contact.Api.Model;
using Microsoft.AspNetCore.Mvc;



namespace Contact.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class ContactsController(
    IContactService contactService,
    IMapper mapper) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContactDto dto, CancellationToken abortionToken = default)
    {
        var model = mapper.Map<CreateContact>(dto);
        var contact = await contactService.CreateContactAsync(model, abortionToken);
        return Ok(mapper.Map<ContactDto>(contact));
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken abortionToken = default)
    {
        var allContacts = await contactService.GetAllContactsAsync(abortionToken);
        return Ok(allContacts.Select(mapper.Map<ContactDto>));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id, CancellationToken abortionToken = default)
    {
        var singleContact = await contactService.GetSingleContactAsync(id, abortionToken);
        return Ok(mapper.Map<ContactDto>(singleContact));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateContactDto dto ,CancellationToken abortionToken = default)
    {
        var updated = await contactService.UpdateContactAsync(id, mapper.Map<UpdateContact>(dto) ,abortionToken);
        return Ok(mapper.Map<ContactDto>(updated));
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateSinglePropertyAsync(int id, [FromBody] PatchContactDto dto ,CancellationToken abortionToken = default)
    {
        var updatedSinglePart = await contactService.UpdateSinglePartOfContactAsync(id, mapper.Map<PatchContact>(dto) ,abortionToken);
        return Ok(mapper.Map<ContactDto>(updatedSinglePart));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactAsync(int id, CancellationToken abortionToken = default)
    {
        await contactService.DeleteContactAsync(id ,abortionToken);
        return Ok($"Contact with id '{id}' is deleted");
    }

}
