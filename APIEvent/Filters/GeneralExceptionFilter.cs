using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace APIEvent.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<GeneralExceptionFilter> _logger;

        public GeneralExceptionFilter(ILogger<GeneralExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var problem500 = new ProblemDetails
            {
                Status = 500,
                Title = "Erro inesperado.",
                Detail = "Erro inesperado. Tente novamente",
                Type = context.Exception.GetType().Name
            };

            var problem503 = new ProblemDetails
            {
                Status = 503,
                Title = "Serviço Indisponível",
                Detail = "Erro inesperado ao se comunicar com o banco de dados",
                Type = context.Exception.GetType().Name
            };

            var problem417 = new ProblemDetails
            {
                Status = 417,
                Title = "Erro de Referência Nula",
                Detail = "Erro inesperado no sistema",
                Type = context.Exception.GetType().Name
            };

            _logger.Log(LogLevel.Error, $"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}");

            switch (context.Exception)
            {
                case SqlException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    context.Result = new ObjectResult(problem503);
                    break;
                case NullReferenceException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status417ExpectationFailed;
                    context.Result = new ObjectResult(problem417);
                    break;
                case ArgumentException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem500);
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem500);
                    break;
            }
        }
    }
}
