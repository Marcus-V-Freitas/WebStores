using DLL.BLL.Models;
using Newtonsoft.Json;

namespace DLL.BLL.Services.Sessao
{
    public class LoginCliente
    {
        private readonly string Key = "Login.Cliente";
        private Sessao _sessao;

        public LoginCliente(Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Cliente cliente)
        {
            string clienteJson = JsonConvert.SerializeObject(cliente,
                                                            Formatting.None, new JsonSerializerSettings()
                                                            {
                                                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                            });

            _sessao.Cadastrar(Key, clienteJson);
        }

        public Cliente GetCliente()
        {
            if (_sessao.Existe(Key))
            {
                string clienteJson = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Cliente>(clienteJson);
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _sessao.Remover(Key);
        }


    }
}
