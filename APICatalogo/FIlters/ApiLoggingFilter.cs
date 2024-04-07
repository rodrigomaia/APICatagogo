using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiLoggingFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"###### {context.ActionDescriptor.DisplayName} ################");
        Console.WriteLine($"Executando OnActionExecuting [{DateTime.Now.ToLongTimeString()}]");
        Console.WriteLine($"ModelState: {context.ModelState.IsValid}");
        Console.WriteLine("###########################################################");        
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"###### {context.ActionDescriptor.DisplayName} ################");
        Console.WriteLine($"Executando ActionExecutedContext [{DateTime.Now.ToLongTimeString()}]");
        Console.WriteLine($"Status Code: {context.HttpContext.Response.StatusCode}");
        Console.WriteLine("###########################################################");
    }
}