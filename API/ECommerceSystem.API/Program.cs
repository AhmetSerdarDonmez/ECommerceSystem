using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
using System.Text;
// using FluentValidation.AspNetCore;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


 void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp",
            builder => builder
                .WithOrigins("http://localhost:50619") // Adjust to your React app's origin
                .AllowAnyMethod()
                .AllowAnyHeader());
    });

    services.AddControllers();
}




// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");   


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Test}/{action=TestAction}/{id?}");


app.Run();
