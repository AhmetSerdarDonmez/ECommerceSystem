
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence;

var builder = WebApplication.CreateBuilder(args);

// --- Configure MVC and FluentValidation ---


// --- Configure EF Core with PostgreSQL ---
// Here we retrieve the connection string using builder.Configuration.GetConnectionString("DefaultConnection").
// This method looks for the "ConnectionStrings" section in your appsettings.json.


builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddPersistenceServices();


var app = builder.Build();

// --- Configure the HTTP request pipeline ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization middleware.
app.UseAuthentication();
app.UseAuthorization();

// Set up default MVC route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
