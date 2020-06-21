using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.BLL.Models;
using DLL.DAL.Repository.Interfaces;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.Extensions.Configuration;

namespace WinStore.Unit_Of_Work
{
    public class WinUnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private Guid _id;

        public WinUnitOfWork(IConfiguration configuration,
            IRepository<Categoria> categoria,
            IRepository<Produto> produto)
        {
            _id = Guid.NewGuid();
            _connection = new SqlConnection(configuration.GetConnectionString("SqlServer"));
            CategoriaRepository = categoria;
            ProdutoRepository = produto;
        }

        public IRepository<Categoria> CategoriaRepository { get; }


        public Guid Id
        {
            get { return _id; }
        }

        public IClienteRepository ClienteRepository { get; }

        public IRepository<ItemVenda> ItemVendaRepository { get; }

        public IRepository<Produto> ProdutoRepository { get; }

        public IRepository<Venda> VendaRepository { get; }

        public IDbConnection Connection()
        {
            if (!_connection.State.Equals(ConnectionState.Open))
                _connection.Open();
            return _connection;
        }

        public IDbTransaction Transaction()
        {
            return _transaction;
        }

        public void Begin()
        {
            _transaction = Connection().BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
                Dispose();
            }
            catch (Exception)
            {

                Roolback();
                Dispose();
            }
            finally
            {
                _connection.Close();
            }

        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
            _connection.Close();
        }

        public void Roolback()
        {

            _transaction.Rollback();
            Dispose();

        }
    }
}
