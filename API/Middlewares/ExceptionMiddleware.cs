using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode httpStatusCode = (exception is AppException) ? ((AppException)exception).StatusCode : HttpStatusCode.InternalServerError;

            context.Response.StatusCode = (int)httpStatusCode;

            var result = JsonSerializer.Serialize(new { message = exception?.Message });

            await context.Response.WriteAsync(result);
        }
    }
}
