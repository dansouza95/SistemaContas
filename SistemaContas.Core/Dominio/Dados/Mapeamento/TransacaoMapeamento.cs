using FluentNHibernate.Mapping;
using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Core.Dominio.Dados.Mapeamento
{
    public class TransacaoMapeamento:ClassMap<Transacao>
    {
        public TransacaoMapeamento()
        {
            Id(x => x.IdTransacao).GeneratedBy.Identity();
            Map(x => x.DataTransacao).Not.Nullable();
            Map(x => x.ValorTransacao).Not.Nullable().Precision(2);
            Map(x => x.TipoTransacao).Not.Nullable();

            References(x => x.Cliente).Not.Nullable();
            References(x => x.Conta).Not.Nullable();
            References(x => x.Movimentacao).Not.Nullable();
        }
    }
}
