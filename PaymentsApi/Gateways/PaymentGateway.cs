using PaymentsApi.DTOs;
using PaymentsApi.Models;
using PaymentsApi.Services;

namespace PaymentsApi.Gateways
{
    public class PaymentGateway : IPaymentGateway
    {
        private readonly IPaymentService _paymentService;

        public PaymentGateway(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<Payment> CreatePaymentAsync(CreatePaymentRequestDto request)
        {
            ValidateBusinessRules(request);

            var payment = new Payment
            {
                CustomerId = request.CustomerId,
                ServiceProvider = request.ServiceProvider,
                Amount = request.Amount
                // Status y CreatedAt se setean por defecto en el modelo
            };

            return await _paymentService.SaveAsync(payment);
        }

        private static void ValidateBusinessRules(CreatePaymentRequestDto request)
        {
            if (request.CustomerId == Guid.Empty)
                throw new ArgumentException("CustomerId inválido.");

            if (request.Amount <= 0)
                throw new ArgumentException("El monto debe ser mayor a cero.");

            if (request.Amount > 1500)
                throw new ArgumentException("El monto no puede ser mayor a 1500 Bs.");

            if (IsAmountInDollars(request))
                throw new ArgumentException("No se permiten pagos en dólares.");
        }

        private static bool IsAmountInDollars(CreatePaymentRequestDto request)
        {
            return request.ServiceProvider
                .Contains("USD", StringComparison.OrdinalIgnoreCase)
                || request.ServiceProvider
                .Contains("DOLAR", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<IList<Payment>> GetPaymentsByCustomerAsync(Guid customerId)
        {
            return await _paymentService.GetByCustomerAsync(customerId);
        }
    }
}
