using System.Net;
using System.Text.Json;
using Truck.Registration.Application.Exceptions;

namespace Truck.Registration.Api.GlobalErrorHandler
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var resultJson = string.Empty;

                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case CustomValidationException err:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        resultJson = JsonSerializer.Serialize(new { erros = err.ErrosMessage });
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        resultJson = JsonSerializer.Serialize(new { message = error?.Message });
                        break;
                }

                await response.WriteAsync(resultJson);
            }
        }
    }
}