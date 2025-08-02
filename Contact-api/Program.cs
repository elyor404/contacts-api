using System.Text.Json.Serialization;
using Contact.Api.Abstraction;
using Contact.Api.Dtos;
using Contact.Api.Services;
using Contact.Api.Validators;
using FluentValidation;
using Contact.Api.Filter;
using Contact.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddFluentValidationAsyncAutoValidation()
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
        jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddSingleton<IContactService, ContactService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IValidator<CreateContactDto>, CreateContactValidator>();
builder.Services.AddScoped<IValidator<UpdateContactDto>, UpdateContactValidator>();
builder.Services.AddScoped<IValidator<PatchContactDto>, PatchContactValidator>();


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
