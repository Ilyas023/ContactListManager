using ContactListManager.DAL.Models;
using ContactListManager.DAL.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContactListManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UpdateContactController : Controller
{
    private readonly ILogger<UpdateContactController> _logger;
    private readonly IContactListManagerService _contactListManagerService;

    public UpdateContactController(ILogger<UpdateContactController> logger, IContactListManagerService contactListManagerService)
    {
        _logger = logger;
        _contactListManagerService = contactListManagerService;
    }

    [HttpPut("update-contact-by-id/{id}")]
    public async Task<ActionResult<ContactIsFinded>> UpdateContactByIdAsync(int id, Contact contactUpd)
    {
        return await _contactListManagerService.UpdateContactByIdAsync(id, contactUpd);
    }
}
