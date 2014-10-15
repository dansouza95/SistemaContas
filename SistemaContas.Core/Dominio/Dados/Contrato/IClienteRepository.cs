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
        bool VerificarCadastro(string username);
    }
}
