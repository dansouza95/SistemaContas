using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroContas.Core.Dominio.Dados.Contrato
{
    public interface IContaRepository
    {
        void CadastraConta(Conta conta);
        List<Conta> PegarContas(int id);
        Conta PegarContaPorId(int id);
        void AtualizarConta(Conta conta);
        List<Conta> ContasEmAberto(int id);
        List<Conta> ContasFinalizadas(int id);
        Double PegarExtrato(int id);
        Double Creditos(int id);
        Double Debitos(int id);
        void EditarConta(Conta conta);
        void DeletarConta(Conta conta);
    }
}
