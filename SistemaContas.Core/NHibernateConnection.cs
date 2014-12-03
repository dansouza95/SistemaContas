using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Core
{
    public class NHibernateConnection
    {
        public static ISession OpenSession()
        {
            try
            {
                ISessionFactory sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("ContasConnection"))
                    .ShowSql().FormatSql()).Mappings(map => map.FluentMappings.AddFromAssemblyOf<Cliente>())
                    .ExposeConfiguration(config => new SchemaExport(config).Create(false, false))
                    .BuildSessionFactory();
                return sessionFactory.OpenSession();
            }
            catch
            {
                ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("ContasConnection"))
                .ShowSql().FormatSql()).Mappings(map => map.FluentMappings.AddFromAssemblyOf<Cliente>())
                .ExposeConfiguration(config => new SchemaExport(config).Create(true, true))
                .BuildSessionFactory();
                return sessionFactory.OpenSession();
            }
        }
        
    }
}
