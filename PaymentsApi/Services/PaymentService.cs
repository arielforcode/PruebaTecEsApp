using NHibernate;
using PaymentsApi.Models;
using NHibernate.Linq;

namespace PaymentsApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ISessionFactory _sessionFactory;

        public PaymentService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public async Task<Payment> SaveAsync(Payment payment)
        {
            using var session = _sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            await session.SaveAsync(payment);
            await transaction.CommitAsync();

            return payment;
        }

        public async Task<IList<Payment>> GetByCustomerAsync(Guid customerId)
        {
            if (customerId == Guid.Empty)
                return new List<Payment>();
            using var session = _sessionFactory.OpenSession();

            return await session.Query<Payment>()
                .Where(p => p.CustomerId == customerId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
