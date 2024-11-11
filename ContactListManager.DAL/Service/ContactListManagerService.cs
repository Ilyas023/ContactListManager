using ContactListManager.DAL.Context;
using ContactListManager.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Web.Http.Results;

namespace ContactListManager.DAL.Service;

public class ContactListManagerService : IContactListManagerService
{
    private readonly ContactListManagerDbContext _context;
    private readonly ILogger<ContactListManagerService> _logger;

    public ContactListManagerService(ILogger<ContactListManagerService> logger, ContactListManagerDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateContactAsync(Contact contact)
    {
        try
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) {
            _logger.LogError("Error: {ex.Message}", ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<ContactIsFinded> DeleteContactByIdAsync(int id)
    {
        try
        {
            var contact = await _context.Contacts.FindAsync(id);

            ContactIsFinded contactIsFinded = new ContactIsFinded
            {
                Contact = contact,
                IsFinded = contact != null
            };

            if (contactIsFinded.IsFinded == false)
            {
                contactIsFinded.Message = "Contact dont finded";
                return contactIsFinded;
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            contactIsFinded.Message = "Contact deleted successfully";
            return contactIsFinded;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {ex.Message}", ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Contact>> GetAllContactsAsync()
    {
        return await _context.Contacts.ToListAsync();
    }

    public async Task<ContactIsFinded> GetContactByIdAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);

        ContactIsFinded contactIsFinded = new ContactIsFinded
        {
            Contact = contact,
            IsFinded = contact != null
        };

        if (contactIsFinded.IsFinded == false)
        {
            contactIsFinded.Message = "Contact dont finded";
            return contactIsFinded;
        }

        return contactIsFinded;
    }

    public async Task<ContactIsFinded> UpdateContactById(int id, Contact contactUpd)
    {
        var contactFromContext = await _context.Contacts.FindAsync(id);

        ContactIsFinded contactIsFinded = new ContactIsFinded
        {
            Contact = contactFromContext,
            IsFinded = contactFromContext != null,
        };

        if (contactIsFinded.IsFinded == false && contactFromContext == null)
        {
            contactIsFinded.Message = "Contact dont finded";
            return contactIsFinded;
        }

        contactFromContext.Name = contactUpd.Name;
        contactFromContext.Email = contactUpd.Email;
        contactFromContext.PhoneNumber = contactUpd.PhoneNumber;
        
        await _context.SaveChangesAsync();

        contactIsFinded = new ContactIsFinded
        {
            Contact = contactFromContext,
            IsFinded = true,
            Message = "Contact updated successfully"
        };

        return contactIsFinded;
    }
}
