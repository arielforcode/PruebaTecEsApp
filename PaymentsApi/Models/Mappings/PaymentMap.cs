using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PaymentsApi.Models.Mappings
{
    public class PaymentMap : ClassMapping<Payment>
    {
        public PaymentMap()
        {
            Table("Payments");

            Id(x => x.PaymentId, m =>
            {
                m.Generator(Generators.GuidComb);
            });

            Property(x => x.CustomerId, m =>
            {
                m.NotNullable(true);
            });

            Property(x => x.ServiceProvider, m =>
            {
                m.Length(200);
                m.NotNullable(true);
            });

            Property(x => x.Amount, m =>
            {
                m.NotNullable(true);
            });

            Property(x => x.Status, m =>
            {
                m.NotNullable(true);
                m.Type<NHibernate.Type.EnumStringType<PaymentsApi.Enums.PaymentStatus>>();
            });

            Property(x => x.CreatedAt, m =>
            {
                m.NotNullable(true);
            });
        }
    }
}
