using Microsoft.EntityFrameworkCore;
using Viamatica.Infrastructure.Persistence;
using Viamatica.Application.Interfaces;
using Viamatica.Infrastructure.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ViamaticaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers
builder.Services.AddControllers();

// OpenApi
builder.Services.AddOpenApi(); 

builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// JWT
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();