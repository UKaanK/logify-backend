

using FluentValidation;
using FluentValidation.AspNetCore;
using logifly.api.Middlewares;
using logifly.api.Models;
using logifly.application.Interfaces;
using logifly.application.Services;
using logifly.application.Validators;
using logifly.persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Database Baðlantýsý
builder.Services.AddDbContext<LogiflyDbContext>(options=>options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

//Validator Baðlantýsý
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
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketLogService, TicketLogService>(); // varsa log service için de
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi 


builder.Services.AddEndpointsApiExplorer(); // gerekli
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Logify API",
        Version = "v1",
        Description = "Bu API,destek taleplerinin yönetimini saðlar",
        Contact = new OpenApiContact
        {
            Name = "Umut Kaan Kartaloðlu",
            Email = "umutkaankartaloglu9@gmail.com"
        }
    });
}
    
    ); // swagger servisi

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray());
        var apiError = new ApiException(400, "Validation Failed", errors);
        return new BadRequestObjectResult(apiError);
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Ýstersen ayar da verebilirsin
    //using var scope = app.Services.CreateScope();
    //var db = scope.ServiceProvider.GetRequiredService<LogiflyDbContext>();
    //db.Database.Migrate();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
