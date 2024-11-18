using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ContactListManager.DAL.Models;

public class ContactApiResponse{
    public bool Success { get; set; }
    public object Contact { get; set; }

    public ContactApiResponse(bool success, object contact)
    {
        Success = success;
        Contact = contact;
    }
}
public class Contact
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [EmailAddress(ErrorMessage = "Invalid address")]
    public string? Email { get; set; }

    [Required, Phone(ErrorMessage = "Invalid phone")]
    public string? PhoneNumber { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}
public class ContactIsFinded
{
    public Contact? Contact { get; set; }
    public bool IsFinded { get; set; }
    public string? Message { get; set; }
}
