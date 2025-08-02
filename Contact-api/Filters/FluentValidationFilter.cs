using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contact.Api.Filter;

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

            if (canBeValidated && serviceProvider.GetService(typeof(IValidator<>).MakeGenericType(parameter.ParameterType)) is IValidator validator)
            {
                var subject = context.ActionArguments[parameter.Name];
                var result = await validator.ValidateAsync(new ValidationContext<object?>(subject), context.HttpContext.RequestAborted);
                if (!result.IsValid)
                {
                    result.AddToModelState(context.ModelState, null);
                }
            }
        }
        await next();
    }
}