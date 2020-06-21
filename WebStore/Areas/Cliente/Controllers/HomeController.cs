using DLL.BLL.Services.ExtensionsMethods;
using DLL.BLL.Services.Sessao;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using ClienteModel = DLL.BLL.Models.Cliente;
using Proc = DLL.DAL.Repository.Database.Procedures.ProceduresSQL;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace WebStore.Areas.Cliente.Controllers
{
    [Area(nameof(Cliente))]
    [Route("Cliente/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly LoginCliente _loginCliente;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(LoginCliente loginCliente, IUnitOfWork unitOfWork)
        {
            _loginCliente = loginCliente;
            _unitOfWork = unitOfWork;
            _unitOfWork.ClienteRepository.SetUnitOfWork(_unitOfWork);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(ClienteModel cliente)
        {

            ClienteModel clienteDB = _unitOfWork.ClienteRepository.Login(cliente.Email, cliente.Senha);


            if (clienteDB != null)
            {
                _loginCliente.Login(clienteDB);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                ViewData["MSG_E"] = "Email não existe";
            }

            return View();
        }

        public RedirectToActionResult Logout()
        {
            _loginCliente.Logout();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Use(X =>
                    {
                        X.Begin();
                        _unitOfWork.ClienteRepository.Cadastrar(Proc.Cliente, cliente);

                        X.Commit();
                    });
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            catch (Exception ex)
            {

                TempData["MSG_E"] = ex.Message;
            }

            return View();
        }
    }
}
