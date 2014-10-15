using SistemaContas.Core.Dominio.Entidades;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroContas.Core.Dominio.Dados.Mapeamento
{
    public class ContaMapeamento: ClassMap<Conta>
    {
        public ContaMapeamento()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.DataVencimento).Not.Nullable();
            Map(x => x.StatusConta).Not.Nullable();
            Map(x => x.TipoOperacao).Not.Nullable();
            Map(x => x.ValorConta).Not.Nullable();
            Map(x => x.Descricao).Not.Nullable();
            Map(x => x.CredorOuDevedor).Not.Nullable();
            Map(x => x.UltimaAtualizacao);

            References(x => x.Cliente, "IdCliente").Not.Nullable();

        }

    }
}
