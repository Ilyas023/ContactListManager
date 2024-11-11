using ContactListManager.DAL.Models;
using ContactListManager.DAL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactListManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadContactController : Controller
{
    private readonly ILogger<ReadContactController> _logger;
    private readonly IContactListManagerService _contactListManagerService;

    public ReadContactController(ILogger<ReadContactController> logger, IContactListManagerService contactListManagerService)
    {
        _logger = logger;
        _contactListManagerService = contactListManagerService;
    }

    [HttpGet("get-all-contacts")]
    public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
    {
        return await _contactListManagerService.GetAllContactsAsync();
    }

    [HttpGet("get-contact-by-id/{id}")]
    public async Task<ActionResult<ContactIsFinded>> GetContactById(int id)
    {
        return await _contactListManagerService.GetContactByIdAsync(id);
    }
}
