using CadastroContas.Core.Dominio.Dados.Contrato;
using NHibernate;
using NHibernate.Linq;
using SistemaContas.Core;
using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroContas.Core.Dominio.Dados.Repositorio
{
    public class ContaRepository:IContaRepository
    {

        public void CadastraConta(Conta conta)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(conta);
                        transaction.Commit();
                    }
                    catch(Exception erro)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public List<Conta> PegarContas(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                var lista = session.Query<Conta>().Where(x => x.Cliente.Id == id).ToList();
                return lista;
            }
        }

        public Conta PegarContaPorId(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                var conta = session.Query<Conta>().Where(x => x.Id == id);
                if (conta.Count() > 0)
                {
                    var retorno = conta.Single();
                    return retorno;
                }
                else
                {
                    return null;
                }
            }
        }

        public void AtualizarConta(Conta conta)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(conta);
                        transaction.Commit();
                    }
                    catch (Exception erro)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public List<Conta> ContasEmAberto(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                var lista = session.Query<Conta>().Where(x => x.Cliente.Id == id && x.StatusConta != "Finalizada").OrderBy(x => x.DataVencimento).ToList();
                return lista;
            }
        }

        public List<Conta> ContasFinalizadas(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                var contasFinalizadas = session.Query<Conta>().Where(x => x.Cliente.Id == id && x.StatusConta == "Finalizada").ToList();
                return contasFinalizadas;
            }
        }

        public double PegarExtrato(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                double extrato = 0;
                var contas = session.Query<Conta>().Where(c => c.Cliente.Id == id && c.StatusConta == "Finalizada").ToList();
                if (contas.Count() > 0)
                {
                    foreach (var conta in contas)
                    {
                        if (conta.TipoOperacao == "Débito")
                        {
                            conta.ValorConta *= -1;
                        }
                    }
                    extrato = contas.Sum(s => s.ValorConta);
                }
                return extrato;
            }
        }


        public double Creditos(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                double creditos = 0;
                var contas = session.Query<Conta>().Where(c => c.Cliente.Id == id && c.StatusConta == "Finalizada" && c.TipoOperacao=="Crédito").ToList();
                if (contas.Count() > 0)
                {
                    creditos = contas.Sum(s => s.ValorConta);
                }
                return creditos;
            }
        }

        public double Debitos(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                double debitos = 0;
                var contas = session.Query<Conta>().Where(c => c.Cliente.Id == id && c.StatusConta == "Finalizada" && c.TipoOperacao == "Débito").ToList();
                if (contas.Count() > 0)
                {
                    debitos = contas.Sum(s => s.ValorConta);
                }
                return debitos;
            }
        }


        public void EditarConta(Conta conta)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(conta);
                        transaction.Commit();
                    }
                    catch (Exception erro)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }


        public void DeletarConta(Conta conta)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(conta);
                        transaction.Commit();
                    }
                    catch (Exception erro)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
