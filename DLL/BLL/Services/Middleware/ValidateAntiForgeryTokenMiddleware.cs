using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DLL.BLL.Services.Middleware
{
    public class ValidateAntiForgeryTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAntiforgery _antiforgery;

        public ValidateAntiForgeryTokenMiddleware(RequestDelegate request, IAntiforgery antiforgery)
        {
            _next = request;
            _antiforgery = antiforgery;
        }

        public async Task Invoke(HttpContext context)
        {
            var Cabecalho = context.Request.Headers["x-requested-with"];
            bool AJAX = (Cabecalho == "XMLHttpRequest") ? true : false;

            //Verifica se a requisição veio do próprio site ou é um arquivo do tipo AJAX (Evita envios não autorizados)
            if (HttpMethods.IsPost(context.Request.Method) && !(context.Request.Form.Files.Count == 1 && AJAX))
            {
                await _antiforgery.ValidateRequestAsync(context);
            }

            await _next(context);
        }
    }
}
