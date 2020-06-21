using Dapper;
using DLL.BLL.Models;
using DLL.BLL.Services.Compras;
using DLL.BLL.Services.ExtensionsMethods;
using DLL.BLL.Services.Sessao;
using DLL.DAL.Repository.Interfaces;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Proc = DLL.DAL.Repository.Database.Procedures.ProceduresSQL;

namespace DLL.DAL.Repository.Contracts
{
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {

        //private readonly WebStoreContext _context;
        private readonly CarrinhoCompras _carrinhoCompras;
        private readonly LoginCliente _loginCliente;
        private readonly IUnitOfWork _unitOfWork;

        public VendaRepository(IConfiguration configuration, CarrinhoCompras carrinhoCompras, IUnitOfWork unitOfWork, LoginCliente loginCliente) : base(configuration)
        {
            //_context = context;
            _carrinhoCompras = carrinhoCompras;
            _loginCliente = loginCliente;
            _unitOfWork = unitOfWork;
            _unitOfWork.VendaRepository.SetUnitOfWork(_unitOfWork);
        }

        public void CriarVenda(Venda venda)
        {
            venda.Horario = DateTime.Now;
            venda.Total = _carrinhoCompras.TotalCompra();
            venda.ClienteId = _loginCliente.GetCliente().ID;

            var itemsVenda = new List<dynamic>();

            foreach (var item in _carrinhoCompras.TodosItemsCarrinho())
            {
                itemsVenda.Add(new
                {
                    ID = 0,
                    ProdutoId = item.Produto.ID,
                    Quantidade = item.Quantidade,
                    Preço = item.Produto.Preço,
                    VendaId = venda.Id
                });
            };


            var json = JsonConvert.SerializeObject(itemsVenda);
            DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

            _unitOfWork.Use(x =>
            {
                x.Begin();
                x.VendaRepository.Cadastrar(Proc.Venda, new
                {
                    HORARIO = venda.Horario,
                    TOTAL = venda.Total,
                    CLIENTEID = venda.ClienteId,
                    pitens = dataTable.AsTableValuedParameter("[dbo].[ITENSVENDA]")
                });
                x.Commit();

            });
        }
    }
}
