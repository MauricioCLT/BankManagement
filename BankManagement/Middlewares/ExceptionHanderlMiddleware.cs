using System.Net;
using System.Text.Json;
using Core.Models;

namespace BankManagement.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private const string _contentType = "aplication/json";

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException sqlEx)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = _contentType;

            var error = new ErrorModel
            {
                Message = sqlEx.InnerException?.InnerException?.Message ?? "Ocurrio un Error en la Base de Datos"
            };

            var errorJson = JsonSerializer.Serialize(error);

            await context.Response.WriteAsync(errorJson);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = _contentType;

            var error = new ErrorModel
            {
                Message = ex.Message
            };

            var errorJson = JsonSerializer.Serialize(error);

            await context.Response.WriteAsync(errorJson);
        }
    }
}
