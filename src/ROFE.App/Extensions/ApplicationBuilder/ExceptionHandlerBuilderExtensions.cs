using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ROFE.Application;
using ROFE.Domain;
using ROFE.Infrastructure;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ROFE.App.Extensions.ApplicationBuilder;

public static class ExceptionHandlerBuilderExtensions
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder, string includeErrorDetailInResponse)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>(includeErrorDetailInResponse == "true");
    }
}

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> logger;
    private readonly bool includeErrorDetailInResponse;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, bool includeErrorDetailInResponse)
    {
        next.GetMethodInfo();
        this.logger = logger;
        this.includeErrorDetailInResponse = includeErrorDetailInResponse;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
        var error = exceptionHandlerPathFeature?.Error;
        var details = new JObject();

        switch (error)
        {
            case ArgumentException argExc:
                details = new JObject
                {
                    { "property", argExc.ParamName },
                    { "message", CutArgumentMessage(argExc.Message) }
                };
                break;
            case BusinessException:
            case UseCaseException:
                details = new JObject
                {
                    { "message", CutArgumentMessage(error.Message) }
                };
                break;
            case TechnicalException:
                var msg = "An Technical exception occurred";
                details = new JObject
                {
                    { "message", msg }
                };
                logger.LogError(error, "{Message}: {ErrorMessage}", msg, error.Message);
                break;
            default:
                logger.LogError(error, "Unhandled Exception {error}", error);
                break;
        }

        if (details.HasValues)
        {
            await this.WriteErrorToResponse(httpContext, details);
        }
        else
        {
            await this.WriteUnhandledErrorToResponse(httpContext,
                new Exception($"Unhandled by {nameof(ExceptionHandlerMiddleware)}",
                    error).ToString());
        }
    }

    private static string CutArgumentMessage(string msg)
    {
        return msg.Split(Environment.NewLine).FirstOrDefault();
    }

    private async Task WriteErrorToResponse(HttpContext httpContext, object errorDetail)
    {
        var errorObject = JsonConvert.SerializeObject(new { errors = errorDetail });
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(errorObject, Encoding.UTF8);
    }

    private async Task WriteUnhandledErrorToResponse(HttpContext httpContext, object errorDetail)
    {
        var errorObject = JsonConvert.SerializeObject(
            new
            {
                Error = "Ocurrio un error en la Aplicacion. Por favor contacte al administrador.",
                Detail = this.includeErrorDetailInResponse ? errorDetail : null
            },
            new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            });

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsync(errorObject, Encoding.UTF8);
    }
}
