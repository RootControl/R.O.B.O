using Microsoft.AspNetCore.Diagnostics;

namespace Robo.Api.Configurations;

public static class ExceptionHandler
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                switch (exception)
                {
                    case InvalidOperationException invalidOperationException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            error = invalidOperationException.Message
                        });
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            error = "An error occurred while processing your request."
                        });
                        break;
                }
            });
        });
    }
}