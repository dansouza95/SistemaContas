using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Core.Dominio.Dados.Contrato
{
    public interface ITransacaoRepository
    {
        void SalvarTransacao(Transacao transacao);
        List<Transacao> PegarTransacoes(int id);
        List<Transacao> PegarTransacoesPorConta(int id);
        void ExcluirTransacoes(List<Transacao> lista);
    }
}
