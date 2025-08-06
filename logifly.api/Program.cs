

using FluentValidation;
using FluentValidation.AspNetCore;
using logifly.application.Interfaces;
using logifly.application.Services;
using logifly.application.Validators;
using logifly.persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Database Ba�lant�s�
builder.Services.AddDbContext<LogiflyDbContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

//Validator Ba�lant�s�
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<TicketCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TicketLogCreateDtoValidator>();


// CORS policy ekle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketLogService, TicketLogService>(); // varsa log service i�in de
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi 


builder.Services.AddEndpointsApiExplorer(); // gerekli
builder.Services.AddSwaggerGen(); // swagger servisi

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // �stersen ayar da verebilirsin
    //using var scope = app.Services.CreateScope();
    //var db = scope.ServiceProvider.GetRequiredService<LogiflyDbContext>();
    //db.Database.Migrate();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
