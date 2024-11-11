using ContactListManager.DAL.Context;
using ContactListManager.DAL.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContactListManagerDbContext>(options =>
    options.UseInMemoryDatabase("ContactsDb"));
builder.Services.AddTransient<IContactListManagerService, ContactListManagerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
