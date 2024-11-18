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

            if (contact == null)
            {
                return new ContactIsFinded
                {
                    IsFinded = false,
                    Message = "Contact not found"
                };
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return new ContactIsFinded
            {
                Contact = contact,
                IsFinded = true,
                Message = "Contact deleted successfully"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {ex.Message}", ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Contact>> GetAllContactsAsync(int pageNumber, int pageSize)
    {
        return await _context.Contacts
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
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
        if (contactFromContext == null)
        {
            return new ContactIsFinded
            {
                IsFinded = false,
                Message = "Contact not found"
            };
        }

        contactFromContext.Name = contactUpd.Name;
        contactFromContext.Email = contactUpd.Email;
        contactFromContext.PhoneNumber = contactUpd.PhoneNumber;

        await _context.SaveChangesAsync();
        return new ContactIsFinded
        {
            Contact = contactFromContext,
            IsFinded = true,
            Message = "Contact updated successfully"
        };
    }
}
