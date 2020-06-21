using DLL.BLL.Models;
using DLL.DAL.Repository.Interfaces;
using System;
using System.Data;

namespace DLL.DAL.Repository.Unit_Of_Work.Interfaces
{
    public interface IUnitOfWork
    {
        Guid Id { get; }
        IDbConnection Connection();

        IDbTransaction Transaction();

        void Begin();

        void Commit();

        void Roolback();

        IRepository<Categoria> CategoriaRepository { get; }

        IClienteRepository ClienteRepository { get; }

        IRepository<ItemVenda> ItemVendaRepository { get; }

        IRepository<Produto> ProdutoRepository { get; }

        IRepository<Venda> VendaRepository { get; }
    }
}
