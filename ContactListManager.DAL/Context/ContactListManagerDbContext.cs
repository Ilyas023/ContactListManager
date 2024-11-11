using ContactListManager.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactListManager.DAL.Context;

public class ContactListManagerDbContext: DbContext
{
    public ContactListManagerDbContext(DbContextOptions<ContactListManagerDbContext> options) : base(options) { }
    public DbSet<Contact> Contacts { get; set; }
}
