using DLL.BLL.Models;
using DLL.BLL.ViewModels;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Proc = DLL.DAL.Repository.Database.Procedures.ProceduresSQL;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdutoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.ProdutoRepository.SetUnitOfWork(_unitOfWork);
            _unitOfWork.CategoriaRepository.SetUnitOfWork(_unitOfWork);
        }

        public IActionResult ListaProdutos(int Id)
        {


            IEnumerable<Produto> produtos;
            string categoriaAtual;

            if (Id == 0)
            {
                produtos = _unitOfWork.ProdutoRepository.ObterTodos(Proc.Produto, new { CategoriaId = 0 }).OrderBy(x => x.ID);
                categoriaAtual = "Todos Produtos";
            }
            else
            {
                produtos = _unitOfWork.ProdutoRepository.ObterTodos(Proc.Produto, new { CategoriaId = Id });

                foreach (var produto in produtos)
                {
                    produto.Categoria = _unitOfWork.CategoriaRepository.ObterId(Proc.Categoria, new { ID = produto.CategoriaId });
                }

                categoriaAtual = _unitOfWork.CategoriaRepository.ObterId(Proc.Categoria, new { ID = Id })?.Nome;
            }

            return View(new ProdutoViewModel { Produtos = produtos, CategoriaAtual = categoriaAtual });
        }

        public IActionResult Detalhes(int Id)
        {
            var produto = _unitOfWork.ProdutoRepository.ObterId(Proc.Produto, new { ID = Id });
            if (produto == null)
                return NotFound();

            return View(produto);
        }
    }
}
