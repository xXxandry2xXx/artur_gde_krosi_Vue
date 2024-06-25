using artur_gde_krosi_Vue.Server.Models.ProjecktSetings.Dto.ErrorClasses;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings.Dto.ExceptionModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text.Json;


namespace artur_gde_krosi_Vue.Server.Middlewares
{
    public class ExceptionHandingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionHandingMiddleware> _logger;

        public ExceptionHandingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                 await _requestDelegate(httpContext);
            }
            catch(AllowedOnMailException ex)
            {
                await HandleExceptionClientAsync(httpContext,
                    HttpStatusCode.Forbidden,
                    ex.Message);
            }
            catch(PasswordException ex)
            {
                await HandlePasswordExceptionAsync(httpContext,
                    ex.Message,
                    HttpStatusCode.BadRequest,
                    ex.errors);
            }
            catch(ArgumentException ex)
            {
                await HandleExceptionClientAsync(httpContext,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                await HandleExceptionAsync(httpContext,
                    ex.Message,
                    HttpStatusCode.BadRequest,
                    "Внутренняя ошибка сервера");
            }
        }

        private async Task HandlePasswordExceptionAsync(HttpContext httpContent
            ,string exMsg
            ,HttpStatusCode statusCode
            , IEnumerable<IdentityError> errorsEX)
        {
            _logger.LogError(exMsg);

            HttpResponse response = httpContent.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            PasswordDto errorDto = new()
            {
                errors = errorsEX,
                StatusCode = (int)statusCode
            };

            await response.WriteAsJsonAsync(errorDto);
        }
        private async Task HandleExceptionAsync(HttpContext httpContent
            ,string exMsg
            ,HttpStatusCode statusCode
            ,string message)
        {
            _logger.LogError(exMsg);

            HttpResponse response = httpContent.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            ErrorDto errorDto = new()
            {
                Message = message,
                StatusCode = (int)statusCode
            };

            await response.WriteAsJsonAsync(errorDto);
        }
        private async Task HandleExceptionClientAsync(HttpContext httpContent
            ,HttpStatusCode statusCode
            ,string message)
        {

            HttpResponse response = httpContent.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            ErrorDto errorDto = new()
            {
                Message = message,
                StatusCode = (int)statusCode
            };

            await response.WriteAsJsonAsync(errorDto);
        }
    }
}
