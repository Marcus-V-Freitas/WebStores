using Microsoft.AspNetCore.Http;

namespace DLL.BLL.Services.Sessao
{
    public class Sessao
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public Sessao(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;

        }

        public void Cadastrar(string key, string valor)
        {
            _contextAccessor.HttpContext.Session.SetString(key, valor);
        }

        public void Atualizar(string key, string valor)
        {
            if (Existe(key))
            {
                Remover(key);
            }
            Cadastrar(key, valor);
        }


        public void Remover(string Key)
        {
            _contextAccessor.HttpContext.Session.Remove(Key);
        }

        public string Consultar(string Key)
        {
            return _contextAccessor.HttpContext.Session.GetString(Key);
        }

        public bool Existe(string Key)
        {
            if (_contextAccessor.HttpContext.Session.GetString(Key) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RemoverTodos()
        {
            _contextAccessor.HttpContext.Session.Clear();
        }
    }
}
