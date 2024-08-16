namespace Api.Handlers
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using DomainModel;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;

    public sealed class ExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;

        public ExceptionHandler(RequestDelegate next, IWebHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            string errorMessage = env.IsProduction() ? "Internal server error" : "Exception: " + exception.Message;
            Error error = Errors.General.InternalServerError(errorMessage);
            Envelope envelope = Envelope.Error(error, null);
            string result = JsonSerializer.Serialize(envelope);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}
