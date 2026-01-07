using PaymentsApi.Models;

namespace PaymentsApi.Services
{
    public interface IPaymentService
    {
        Task<Payment> SaveAsync(Payment payment);
        Task<IList<Payment>> GetByCustomerAsync(Guid customerId);
    }
}
