using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contacts.Api.Filters;

public class AsyncAutoValidation(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public static int OrderLowerThanModelStateInvalidFilter => -2001;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var parameter in context.ActionDescriptor.Parameters)
        {
            var isParameterFromBodyOrQuery = parameter.BindingInfo?.BindingSource == BindingSource.Body
                || parameter.BindingInfo?.BindingSource == BindingSource.Query;

            var canBeValidated = isParameterFromBodyOrQuery && parameter.ParameterType.IsClass;

            if (canBeValidated &&
                context.ActionArguments.TryGetValue(parameter.Name, out var subject) &&
                subject is not null &&
                serviceProvider.GetService(typeof(IValidator<>).MakeGenericType(parameter.ParameterType)) is IValidator validator)
            {
                var idProp = parameter.ParameterType.GetProperty("Id");
                if (idProp is not null &&
                    context.RouteData.Values.TryGetValue("id", out var idValue) &&
                    int.TryParse(idValue?.ToString(), out var id))
                {
                    idProp.SetValue(subject, id);
                }

                var result = await validator.ValidateAsync(
                    new ValidationContext<object?>(subject),
                    context.HttpContext.RequestAborted
                );

                if (!result.IsValid)
                {
                    result.AddToModelState(context.ModelState, null);
                }
            }
        }

        await next();
    }
}
