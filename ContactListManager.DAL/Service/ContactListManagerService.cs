using ContactListManager.DAL.Context;
using ContactListManager.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Numerics;
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

    //[CREATE]
    public async Task CreateContactAsync(Contact contact)
    {
        try
        {
            _logger.LogInformation("Creating a new contact: {Name}, {Email}, {PhoneNumber}", contact.Name, contact.Email, contact.PhoneNumber);

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Contact created successfully with ID: {Id}", contact.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while creating a contact: {Message}", ex.Message);
            throw new Exception(ex.Message);
        }
    }

    //[DELETE]
    public async Task<ContactIsFinded> DeleteContactByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete contact with ID: {Id}", id);

            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                _logger.LogWarning("Contact with ID: {Id} not found for deletion", id);
                return new ContactIsFinded
                {
                    IsFinded = false,
                    Message = "Contact not found"
                };
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Contact with ID: {Id} deleted successfully", id);
            return new ContactIsFinded
            {
                Contact = contact,
                IsFinded = true,
                Message = "Contact deleted successfully"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while deleting contact with ID: {Id}: {Message}", id, ex.Message);
            throw new Exception(ex.Message);
        }
    }

    //[READ]
    public async Task<PaginatedList<Contact>> GetAllContactsAsync(int pageIndex, int pageSize)
    {
        _logger.LogInformation("Fetching contacts for page {PageIndex} with page size {PageSize}", pageIndex, pageSize);

        var contacts = await _context.Contacts
            .OrderBy(b => b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var count = await _context.Contacts.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        _logger.LogInformation("Fetched {Count} contacts for page {PageIndex} of {TotalPages}", contacts.Count, pageIndex, totalPages);

        return new PaginatedList<Contact>(contacts, pageIndex, totalPages);
    }

    //[GET BY ID]
    public async Task<ContactIsFinded> GetContactByIdAsync(int id)
    {
        _logger.LogInformation("Fetching contact with ID: {Id}", id);

        var contact = await _context.Contacts.FindAsync(id);

        if (contact == null)
        {
            _logger.LogWarning("Contact with ID: {Id} not found", id);
            return new ContactIsFinded
            {
                IsFinded = false,
                Message = "Contact not found"
            };
        }

        _logger.LogInformation("Contact with ID: {Id} found successfully", id);
        return new ContactIsFinded
        {
            Contact = contact,
            IsFinded = true,
            Message = "Contact retrieved successfully"
        };
    }

    //[UPDATE]
    public async Task<ContactIsFinded> UpdateContactByIdAsync(int id, Contact contactUpd)
    {
        _logger.LogInformation("Attempting to update contact with ID: {Id}", id);

        var contactFromContext = await _context.Contacts.FindAsync(id);
        if (contactFromContext == null)
        {
            _logger.LogWarning("Contact with ID: {Id} not found for update", id);
            return new ContactIsFinded
            {
                IsFinded = false,
                Message = "Contact not found"
            };
        }

        _logger.LogInformation("Updating contact with ID: {Id}", id);

        contactFromContext.Name = contactUpd.Name;
        contactFromContext.Email = contactUpd.Email;
        contactFromContext.PhoneNumber = contactUpd.PhoneNumber;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Contact with ID: {Id} updated successfully", id);
        return new ContactIsFinded
        {
            Contact = contactFromContext,
            IsFinded = true,
            Message = "Contact updated successfully"
        };
    }
}

