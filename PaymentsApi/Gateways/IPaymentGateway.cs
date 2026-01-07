using PaymentsApi.DTOs;
using PaymentsApi.Models;

namespace PaymentsApi.Gateways
{
    public interface IPaymentGateway
    {
        Task<Payment> CreatePaymentAsync(CreatePaymentRequestDto request);
        Task<IList<Payment>> GetPaymentsByCustomerAsync(Guid customerId);
    }
}
