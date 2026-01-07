using NHibernate;
using NHibernate.Mapping.ByCode;
using PaymentsApi.Models.Mappings;
using NHibernate.Cfg;

namespace PaymentsApi.Services.Infrastructure
{
    public static class NHibernateHelper
    {
        public static ISessionFactory CreateSessionFactory(string connectionString)
        {
            var configuration = new Configuration();

            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
                db.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>();
                db.LogSqlInConsole = true;
                db.LogFormattedSql = true;
            });

            var mapper = new ModelMapper();
            mapper.AddMapping<PaymentMap>();

            configuration.AddMapping(
                mapper.CompileMappingForAllExplicitlyAddedEntities()
            );

            return configuration.BuildSessionFactory();
        }
    }
}
