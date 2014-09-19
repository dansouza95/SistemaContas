using NHibernate;
using SistemaContas.Core.Dominio.Dados.Contrato;
using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using CadastroContas.Core.Dominio.Dados.Contrato;

namespace SistemaContas.Core.Dominio.Dados.Repositorio
{
    public class MovimentacaoRepository:IMovimentacaoRepository
    {
        public void SalvarOuAtualizarMovimentacao(Entidades.Movimentacao movimentacao)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(movimentacao);
                        tran.Commit();
                    }
                    catch (Exception erro)
                    {
                        tran.Rollback();
                    }
                }
            }
        }


        public Movimentacao PegarMovimentacao(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                try
                {
                    var movimentacao = session.Query<Movimentacao>().Where(x => x.Conta.Id == id);
                    if (movimentacao.Count() > 0)
                    {
                        var parcelas = movimentacao.Min(x => x.ParcelasRestantes);
                        var pg = movimentacao.Where(x => x.ParcelasRestantes == parcelas).SingleOrDefault();
                        return pg;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception erro)
                {
                    return null;
                }
            }
        }



        public void SalvarMovimentacao(Movimentacao movimentacao)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(movimentacao);
                        tran.Commit();
                    }
                    catch (Exception erro)
                    {
                        tran.Rollback();
                    }
                }
            }
        }
    }
}
