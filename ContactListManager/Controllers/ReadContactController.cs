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
    public async Task<ActionResult<ContactApiResponse>> GetAllContacts(int pageIndex = 1, int pageSize = 10)
    {
        var contacts = await _contactListManagerService.GetAllContactsAsync(pageIndex, pageSize);
        return new ContactApiResponse(true, contacts);
    }

    [HttpGet("get-contact-by-id/{id}")]
    public async Task<ActionResult<ContactApiResponse>> GetContactById(int id)
    {
        var contact = await _contactListManagerService.GetContactByIdAsync(id);
        return new ContactApiResponse(true, contact);
    }
}
