using Dapper;
using DLL.BLL.Models.Enums;
using DLL.BLL.Models.Exceptions;
using DLL.BLL.Services.ExtensionsMethods;
using DLL.DAL.Repository.Interfaces;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using X.PagedList;
using static Dapper.SqlMapper;

namespace DLL.DAL.Repository.Contracts
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IConfiguration _conf;
        private IUnitOfWork _unitOfWork;

        public Repository(IConfiguration configuration)
        {
            _conf = configuration;
        }

        public void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Cadastrar o objeto na base de dados
        /// </summary>
        /// <param name="procedure"> Nome da Procedure </param>
        /// <param name="parameters"> Parâmetros da Procedure </param>
        /// <returns> Total de Linhas Afetadas </returns>
        public int Cadastrar(string procedure, object parameters)
        {
            try
            {
                if (string.IsNullOrEmpty(procedure) || parameters.Equals(null))
                {
                    throw new ArgumentNullException(nameof(procedure));
                }


                var result = _unitOfWork.Connection().Execute(sql: procedure,
                                                                param: (object)parameters.Merge(new { @pTipo = CRUD.Cadastrar }),
                                                                commandType: CommandType.StoredProcedure,
                                                                transaction: _unitOfWork.Transaction());

                return result > 0 ? result : throw new RepositoryException();

            }
            catch (RepositoryException)
            {
                _unitOfWork.Roolback();
                throw new Exception("Registro não Gravado");
            }
            catch (Exception ex)
            {
                _unitOfWork.Roolback();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Recupera todos ou os especificados Objetos na base de dados
        /// </summary>
        /// <param name="pagina"> Número da Página Atual </param>
        /// <param name="procedure"> Nome da Procedure </param>
        /// <param name="parameters"> Parâmetros da Procedure </param>
        /// <returns> Lista Páginada com os objetos </returns>
        public async Task<IPagedList<T>> ObterTodos(int? pagina, string procedure, object parameters)
        {

            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            try
            {
                var marcas = _unitOfWork.Connection().Query<T>(sql: procedure,
                                                               param: (object)parameters.Merge(new { @pTipo = CRUD.ObterTodos }),
                                                               commandType: CommandType.StoredProcedure);


                return await marcas.ToPagedListAsync<T>(numeroPagina, RegistroPorPagina);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Recucuperar o objeto selecionado na Base de dados
        /// </summary>
        /// <param name="procedure"> Nome da Procedure </param>
        /// <param name="id"> Parâmetro da procedure </param>
        /// <returns> Objeto </returns>
        public T ObterId(string procedure, object id)
        {
            try
            {
                return _unitOfWork.Connection().QuerySingle<T>(sql: procedure,
                                                                    param: (object)id.Merge(new { @pTipo = CRUD.ObterId }),
                                                                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Atualizar o Objeto na Base de dados
        /// </summary>
        /// <param name="procedure"> Nome da procedure </param>
        /// <param name="parameters"> Parâmetros da procedure </param>
        /// <returns> Total Linhas Afetadas </returns>
        public int Atualizar(string procedure, object parameters)
        {
            try
            {

                var result = _unitOfWork.Connection().Execute(sql: procedure,
                                                              param: (object)parameters.Merge(new { @pTipo = CRUD.Atualizar }),
                                                              transaction: _unitOfWork.Transaction(),
                                                              commandType: CommandType.StoredProcedure);

                return result;

            }
            catch (RepositoryException)
            {
                throw new Exception("Registro não Atualizado");
            }
            catch (Exception ex)
            {
                _unitOfWork.Roolback();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Exclusão do objeto na base de dados
        /// </summary>
        /// <param name="procedure"> Nome da procedure </param>
        /// <param name="id"> Parâmetro da procedure </param>
        /// <returns> Total de Linhas Afetadas </returns>
        public int Excluir(string procedure, object id)
        {
            try
            {
                var result = _unitOfWork.Connection().Execute(sql: procedure,
                                                              param: (object)id.Merge(new { @pTipo = CRUD.Excluir }),
                                                              transaction: _unitOfWork.Transaction(),
                                                              commandType: CommandType.StoredProcedure);
                //transation.Commit();
                return result > 0 ? result : throw new RepositoryException();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException("Registro não Excluído");
            }

            catch (Exception ex)
            {
                _unitOfWork.Roolback();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Recuperar consulta que retorna 2 selects da base de dados
        /// </summary>
        /// <typeparam name="T1"> Primeiro Tipo retornado </typeparam>
        /// <typeparam name="T2"> Segundo Tipo retornado </typeparam>
        /// <param name="procedure"> Nome da procedure  </param>
        /// <param name="parameters"> Parâmetros da procedure </param>
        /// <param name="func1"> 1 Função retornada (select) </param>
        /// <param name="func2"> 2 Função retornada (select) </param>
        /// <returns> Lista composta pelos dois tipos (2 selects) </returns>
        public Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultiple<T1, T2>(string procedure, object parameters,
                                          Func<GridReader, IEnumerable<T1>> func1,
                                          Func<GridReader, IEnumerable<T2>> func2)
        {
            var objs = getMultiple(procedure, parameters, func1, func2);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>);
        }

        /// <summary>
        /// Recuperar consulta que retorna 3 selects da base de dados
        /// </summary>
        /// <typeparam name="T1"> Primeiro Tipo retornado </typeparam>
        /// <typeparam name="T2"> Segundo Tipo retornado </typeparam>
        /// <typeparam name="T3"> Segundo Tipo retornado </typeparam>
        /// <param name="procedure"> Nome da procedure  </param>
        /// <param name="parameters"> Parâmetros da procedure </param>
        /// <param name="func1"> 1 Função retornada (select) </param>
        /// <param name="func2"> 2 Função retornada (select) </param>
        ///  /// <param name="func3"> 3 Função retornada (select) </param>
        /// <returns> Lista composta pelos três tipos (3 selects) </returns>
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultiple<T1, T2, T3>(string procedure, object parameters,
                                        Func<GridReader, IEnumerable<T1>> func1,
                                        Func<GridReader, IEnumerable<T2>> func2,
                                        Func<GridReader, IEnumerable<T3>> func3)
        {
            var objs = getMultiple(procedure, parameters, func1, func2, func3);
            return Tuple.Create(objs[0] as IEnumerable<T1>, objs[1] as IEnumerable<T2>, objs[2] as IEnumerable<T3>);
        }

        /// <summary>
        ///  Configuração interna para recuperar consultas com múltíplos parâmetros
        /// </summary>
        /// <param name="procedure"> Nome da procedure </param>
        /// <param name="parameters"> Parâmetros da procedure </param>
        /// <param name="readerFuncs"> Tipo por Referência que retorna as linhas recuperadas da consulta </param>
        /// <returns> Lista com múltiplos selects </returns>
        private List<object> getMultiple(string procedure, object parameters, params Func<GridReader, object>[] readerFuncs)
        {
            var returnResults = new List<object>();

            var gridReader = _unitOfWork.Connection().QueryMultiple(procedure, parameters, commandType: CommandType.StoredProcedure);

            foreach (var readerFunc in readerFuncs)
            {
                var obj = readerFunc(gridReader);
                returnResults.Add(obj);
            }

            return returnResults;
        }

        public IEnumerable<T> ObterTodos(string procedure, object parameters)
        {
            try
            {

                return _unitOfWork.Connection().Query<T>(sql: procedure,
                                                         param: (object)parameters.Merge(new { @pTipo = CRUD.ObterTodos }),
                                                         commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                _unitOfWork.Roolback();
                throw new Exception(ex.Message);
            }
        }
    }
}
