using DLL.BLL.Models;
using DLL.DAL.Repository.Contracts;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinStore.Configuracao;

namespace WinStore.Unit_Of_Work
{
    public static class Inicializar
    {
        public static readonly IUnitOfWork _unitOfWork = new WinUnitOfWork(Config.configuration,
      new Repository<Categoria>(Config.configuration),
      new Repository<Produto>(Config.configuration));



        public static IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }
    }
}
