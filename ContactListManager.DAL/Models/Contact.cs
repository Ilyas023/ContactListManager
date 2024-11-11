using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactListManager.DAL.Models;

public class Contact
{
    [JsonIgnore]
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }

    [EmailAddress(ErrorMessage = "Invalid address")]
    public string? Email { get; set; }

    [Required, Phone(ErrorMessage = "Invalid phone")]
    public string? PhoneNumber { get; set; }
}

public class ContactIsFinded
{
    public Contact? Contact { get; set; }
    public bool IsFinded { get; set; }
    public string? Message { get; set; }
}