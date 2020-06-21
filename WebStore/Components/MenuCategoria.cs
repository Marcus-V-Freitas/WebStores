using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Proc = DLL.DAL.Repository.Database.Procedures.ProceduresSQL;

namespace WebStore.Components
{
    public class MenuCategoria : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuCategoria(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.CategoriaRepository.SetUnitOfWork(_unitOfWork);
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _unitOfWork.CategoriaRepository.ObterTodos(Proc.Categoria, new { ID = 0 });

            return View(categorias);
        }
    }
}
