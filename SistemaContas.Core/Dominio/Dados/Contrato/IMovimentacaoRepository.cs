using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Core.Dominio.Dados.Contrato
{
    public interface IMovimentacaoRepository
    {
        void SalvarOuAtualizarMovimentacao(Movimentacao movimentacao);
        Movimentacao PegarMovimentacao(int id);
        void SalvarMovimentacao(Movimentacao movimentacao);
        List<Movimentacao> PegarMovimentacoesNaoFinalizadas();
        void ExcluirMovimentacao(Movimentacao movimentacao);
    }
}
