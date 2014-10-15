using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Core.Dominio.Entidades
{
    public class Conta
    {
        public virtual int Id { get; set; }
        public virtual String CredorOuDevedor { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual String Descricao { get; set; }
        public virtual Double ValorConta { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual String StatusConta { get; set; }
        public virtual String TipoOperacao { get; set; }
        public virtual DateTime UltimaAtualizacao { get; set; }
        public virtual String Valor { get; set; }
    }
}
