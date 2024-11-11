using ContactListManager.DAL.Models;
using ContactListManager.DAL.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContactListManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeleteContactController : Controller
{
    private readonly ILogger<DeleteContactController> _logger;
    private readonly IContactListManagerService _contactListManagerService;

    public DeleteContactController(ILogger<DeleteContactController> logger, IContactListManagerService contactListManagerService)
    {
        _logger = logger;
        _contactListManagerService = contactListManagerService;
    }

    [HttpDelete("delete-contact-by-id/{id}")]
    public async Task<ActionResult<ContactIsFinded>> DeleteContactById(int id)
    {
        return await _contactListManagerService.DeleteContactByIdAsync(id);
    }

}
