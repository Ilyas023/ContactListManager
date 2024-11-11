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
    public async Task<ActionResult<Contact>> PostContact(Contact contact)
    {
        _logger.LogInformation("Started create");

        await _contactListManagerService.CreateContactAsync(contact);

        _logger.LogInformation("Contact created succesfull, {contact.Name} added to contact list.", contact.Name); 

        return CreatedAtAction(
            nameof(ReadContactController.GetAllContacts), 
            "ReadContact", 
            new { id = contact.Id }, 
            contact
        );
    }
}
