using Microsoft.EntityFrameworkCore;
using Viamatica.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ViamaticaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// JWT
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();