using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;
using static Dapper.SqlMapper;

namespace DLL.DAL.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void SetUnitOfWork(IUnitOfWork unitOfWork);

        T ObterId(string procedure, object id);


        int Cadastrar(string procedure, object parameters);

        Task<IPagedList<T>> ObterTodos(int? pagina, string procedure, object parameters);

        IEnumerable<T> ObterTodos(string proceure, object parameters);


        int Atualizar(string procedure, object parameters);

        int Excluir(string procedure, object id);

        Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultiple<T1, T2>(string procedure, object parameters,
                                     Func<GridReader, IEnumerable<T1>> func1,
                                     Func<GridReader, IEnumerable<T2>> func2);


        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultiple<T1, T2, T3>(string procedure, object parameters,
                                      Func<GridReader, IEnumerable<T1>> func1,
                                      Func<GridReader, IEnumerable<T2>> func2,
                                      Func<GridReader, IEnumerable<T3>> func3);
    }
}
