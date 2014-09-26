using NHibernate;
using NHibernate.Linq;
using SistemaContas.Core.Dominio.Dados.Contrato;
using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Core.Dominio.Dados.Repositorio
{
    public class TransacaoRepository:ITransacaoRepository
    {
        public void SalvarTransacao(Entidades.Transacao transacao)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(transacao);
                        transaction.Commit();
                    }
                    catch(Exception erro)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public List<Entidades.Transacao> PegarTransacoes(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                try
                {
                    var listaTransacoes = session.Query<Transacao>().Where(x => x.Cliente.Id == id).OrderBy(x => x.DataTransacao).ToList();
                    return listaTransacoes;
                }
                catch (Exception erro)
                {
                    return null;
                }
            }
        }


        public List<Transacao> PegarTransacoesPorConta(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                var lista = session.Query<Transacao>().Where(x => x.Conta.Id == id).ToList();
                return lista;
            }
        }

        public void ExcluirTransacoes(List<Transacao> lista)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in lista)
                        {
                            session.Delete(item);
                        }
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
