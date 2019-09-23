using CocShop.Core.Constaint;
using CocShop.Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace CocShop.WebAPi.Extentions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var ex = contextFeature?.Error;
                    if (ex != null)
                    {
                        var errorDetail = new ErrorDetail
                        {
                            ErrorMessage = ex.Message
                        };
                        if (ex is EntityNotFoundException)
                        {
                            errorDetail.ErrorCode = (int)MyEnum.ErrorCode.EntityNotFound;
                        }
                        else if (ex is BadRequestException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            errorDetail.ErrorCode = (int)MyEnum.ErrorCode.BadRequest;
                            errorDetail.ErrorMessage = (ex as BadRequestException).Errors;
                        }
                        else if (ex is AccessDeniedException accessDeniedException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            errorDetail.ErrorCode = accessDeniedException.ErrorCode;
                        }
                        else
                        {
                            errorDetail.ErrorCode = (int)MyEnum.ErrorCode.UnknownError;
                        }
                        await context.Response.WriteAsync(errorDetail.ToString());
                    }
                });
            });
        }
    }

    public class ErrorDetail
    {
        public int ErrorCode { get; set; }
        public object ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}