using System.Text.Json.Serialization;
using Contacts.Api.Abstraction;
using Contacts.Api.Dtos;
using Contacts.Api.Services;
using Contacts.Api.Validators;
using FluentValidation;
using Contacts.Api.Filters;
using Contacts.Api.Middlewares;
using Contacts.Api.Repostories.Abstraction;
using Contacts.Api.Repostories;
using Contacts.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddFluentValidationAsyncAutoValidation()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
        jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IValidator<CreateContactDto>, CreateContactValidator>();
builder.Services.AddScoped<IValidator<UpdateContactDto>, UpdateContactValidator>();
builder.Services.AddScoped<IValidator<PatchContactDto>, PatchContactValidator>();

builder.Services.AddDbContext<ContactContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Contact"))
                                                        .UseSnakeCaseNamingConvention());

var app = builder.Build();
app.MapControllers();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();


app.Run();



public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddFluentValidationAsyncAutoValidation(this IMvcBuilder builder)
    {
        return builder.AddMvcOptions(o =>
        {
            o.Filters.Add<AsyncAutoValidation>(AsyncAutoValidation.OrderLowerThanModelStateInvalidFilter);
        });
    }
}
