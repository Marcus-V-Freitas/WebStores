using DLL.BLL.Services.Sessao;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components
{
    public class MenuCliente : ViewComponent
    {
        private readonly LoginCliente _loginCliente;

        public MenuCliente(LoginCliente loginCliente)
        {
            _loginCliente = loginCliente;
        }


        public IViewComponentResult Invoke()
        {
            var cliente = _loginCliente.GetCliente();
            return View(cliente);
        }
    }
}
