using SistemaContas.Core.Dominio.Entidades;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroContas.Core.Dominio.Dados.Mapeamento
{
    public class PagamentoMapeamento:ClassMap<Movimentacao>
    {
        public PagamentoMapeamento()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Tipo).Not.Nullable();
            Map(x => x.Valor).Not.Nullable();
            Map(x => x.NumeroParcelas).Not.Nullable();
            Map(x => x.ValorParcela).Not.Nullable();
            Map(x => x.Status).Not.Nullable();
            Map(x => x.ParcelasRestantes).Not.Nullable();
            Map(x => x.UltimaAtualizacao).Not.Nullable();
            Map(x => x.ValorRestante).Not.Nullable();


            References(x => x.Cliente, "IdCliente").Not.Nullable();
            References(x => x.Conta, "IdConta").Not.Nullable();

        }
    }
}
