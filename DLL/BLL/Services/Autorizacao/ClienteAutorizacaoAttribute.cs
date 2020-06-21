using DLL.BLL.Models;
using DLL.BLL.Services.Sessao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace DLL.BLL.Services.Autorizacao
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private LoginCliente _loginCliente;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginCliente = (LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));
            Cliente cliente = _loginCliente.GetCliente();

            if (cliente == null)
            {
                context.Result = new StatusCodeResult(403);
            }

        }
    }
}
