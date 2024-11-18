using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactListManager.DAL.Service;
using ContactListManager.DAL.Models;


namespace ContactListManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreateContactsController : Controller
{
    private readonly ILogger<CreateContactsController> _logger;
    private readonly IContactListManagerService _contactListManagerService;

    public CreateContactsController(ILogger<CreateContactsController> logger, IContactListManagerService contactListManagerService)
    {
        _logger = logger;
        _contactListManagerService = contactListManagerService;
    }

    [HttpPost("post-contact")]
    public async Task<ActionResult<ContactApiResponse>> PostContact(Contact contact)
    {
        await _contactListManagerService.CreateContactAsync(contact);

        return CreatedAtAction(
            nameof(ReadContactController.GetAllContacts), 
            "ReadContact", 
            new { id = contact.Id },
            new ContactApiResponse(true, contact)
        );
    }
}
