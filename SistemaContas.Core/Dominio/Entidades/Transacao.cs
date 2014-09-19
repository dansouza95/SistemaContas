using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Core.Dominio.Entidades
{
    public class Transacao
    {
        public virtual int IdTransacao { get; set; }
        public virtual Movimentacao Movimentacao{ get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Conta Conta { get; set; }
        public virtual Double ValorTransacao { get; set; }
        public virtual DateTime DataTransacao { get; set; }
        public virtual String TipoTransacao { get; set; }
    }
}
