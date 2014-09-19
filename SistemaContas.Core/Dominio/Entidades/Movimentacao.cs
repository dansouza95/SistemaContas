using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Core.Dominio.Entidades
{
    public class Movimentacao
    {
        public virtual int Id{ get; set; }
        public virtual Cliente Cliente { get;set; }
        public virtual Conta Conta { get; set; }
        public virtual String Tipo { get; set; }
        public virtual Double Valor { get; set; }
        public virtual Double ValorParcela { get; set; }
        public virtual int NumeroParcelas { get; set; }
        public virtual String Status { get; set; }
        public virtual int ParcelasRestantes { get; set; }
        public virtual DateTime UltimaAtualizacao { get; set; }
        public virtual Double ValorRestante { get; set; }
    }
}
