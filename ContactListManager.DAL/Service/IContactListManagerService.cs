using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ContactListManager.DAL.Models;

namespace ContactListManager.DAL.Service;

public interface IContactListManagerService
{
    public Task CreateContactAsync(Contact contact);
    public Task<ContactIsFinded> GetContactByIdAsync(int id);
    public Task<List<Contact>> GetAllContactsAsync();
    public Task<ContactIsFinded> UpdateContactById(int id, Contact contactUpd);
    public Task<ContactIsFinded> DeleteContactByIdAsync(int id);
}
