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
    public class ClienteRepository:IClienteRepository
    {
        public void CadastraCliente(Cliente cliente)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(cliente);
                        tran.Commit();
                    }
                    catch (Exception erro)
                    {
                        tran.Rollback();
                    }
                }
            }
        }

        public int LoginCliente(Cliente cliente)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                cliente = session.Query<Cliente>().Where(x => x.Usuario == cliente.Usuario && x.Senha == cliente.Senha).SingleOrDefault();
                if (cliente != null)
                {
                    return cliente.Id;
                }
                else
                {
                    return 0;
                }
                
            }
        }

        public Cliente PegarClientePorId(int id)
        {
            using (ISession session = NHibernateConnection.OpenSession())
            {
                var cliente = session.Query<Cliente>().Where(x => x.Id == id);
                if (cliente.Count() > 0)
                {
                    var user = cliente.Single();
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
