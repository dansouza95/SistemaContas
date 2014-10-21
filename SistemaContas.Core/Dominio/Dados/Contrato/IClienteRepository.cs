using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroContas.Core.Dominio.Dados.Contrato
{
    public interface IClienteRepository
    {
        void CadastraCliente(Cliente cliente);
        int LoginCliente(Cliente cliente);
        Cliente PegarClientePorId(int id);
<<<<<<< HEAD
        bool VerificarCadastro(Cliente cliente);
=======
        bool VerificarCadastro(string username);
>>>>>>> 5e4e4453edd660e948bbb5fdac76b6e0fb56c9f2
    }
}
