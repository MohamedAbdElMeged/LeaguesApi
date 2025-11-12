using System.Net;
using LeaguesApi.Dtos;
using LeaguesApi.Exceptions;

namespace LeaguesApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            var response = context.Response;
            response.ContentType = "application/json";

            var error = new ErrorResponse
            {
                Message = ex.Message
            };

            // Map exception types to proper HTTP codes
            response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                BadRequestException => (int)HttpStatusCode.BadRequest,
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            error.Code = response.StatusCode switch
            {
                400 => "BAD_REQUEST",
                401 => "UNAUTHORIZED",
                404 => "NOT_FOUND",
                500 => "INTERNAL_ERROR",
                _ => "ERROR"
            };
            
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                error.Details = ex.StackTrace;
            }

            await response.WriteAsJsonAsync(error);
        }
    }
}
