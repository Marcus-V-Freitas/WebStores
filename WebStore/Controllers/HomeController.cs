using DLL.BLL.ViewModels;
using DLL.DAL.Repository.Database;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Proc = DLL.DAL.Repository.Database.Procedures.ProceduresSQL;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {

        //private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly WebStoreContext _context;


        public HomeController(IUnitOfWork unitOfWork, WebStoreContext context) //IProdutoRepository produtoRepository)
        {
            //_produtoRepository = produtoRepository;
            _context = context;
            _unitOfWork = unitOfWork;
            _unitOfWork.ProdutoRepository.SetUnitOfWork(_unitOfWork);
            _unitOfWork.VendaRepository.SetUnitOfWork(_unitOfWork);
            _unitOfWork.ItemVendaRepository.SetUnitOfWork(_unitOfWork);
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                ProdutoAVenda = _unitOfWork.ProdutoRepository.ObterTodos(Proc.Produto, new { @ID = 0 })
                //ProdutoAVenda = _produtoRepository.TodosProdutosVenda
            };



            return View(homeViewModel);
        }
    }
}
