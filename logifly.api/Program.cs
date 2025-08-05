

using Microsoft.EntityFrameworkCore;
using logifly.persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

//Database Baðlantýsý
builder.Services.AddDbContext<LogiflyDbContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<LogiflyDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
