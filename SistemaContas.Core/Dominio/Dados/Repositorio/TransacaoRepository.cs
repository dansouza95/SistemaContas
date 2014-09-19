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


        public double PegarTotalTransacoes(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                try
                {
                    var listaTransacoes = session.Query<Transacao>().Where(x => x.Cliente.Id == id).OrderBy(x => x.DataTransacao).ToList();
                    foreach (var item in listaTransacoes)
                    {
                        if (item.TipoTransacao == "Débito")
                        {
                            item.ValorTransacao *= -1;
                        }
                    }
                    var total = listaTransacoes.Sum(x => x.ValorTransacao);
                    return total;
                    
                }
                catch (Exception erro)
                {
                    return 0;
                }
            }
        }
    }
}
