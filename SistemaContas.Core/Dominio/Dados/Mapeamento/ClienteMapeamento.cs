using SistemaContas.Core.Dominio.Entidades;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroContas.Core.Dominio.Dados.Mapeamento
{
    public class ClienteMapeamento:ClassMap<Cliente>
    {
        public ClienteMapeamento()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nome).Not.Nullable().Length(150);
            Map(x => x.Usuario).Not.Nullable().Length(20);
            Map(x => x.Senha).Not.Nullable().Length(20);
            Map(x => x.Email).Not.Nullable().Length(40);
            Map(x => x.Permissao).Not.Nullable();
            Map(x => x.DataCadastro).Not.Nullable();
            
        }
    }
}
