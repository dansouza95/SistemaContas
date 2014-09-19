using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Core.Dominio.Entidades
{
    public class Cliente
    {
        public virtual int Id { get; set;}
        public virtual String Nome { get; set; }
        public virtual String Usuario { get; set; }
        public virtual String Email { get; set; }
        public virtual String Senha { get; set; }
        public virtual String Permissao { get; set; }
        public virtual DateTime DataCadastro { get; set; }
    }
}
