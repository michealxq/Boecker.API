
using AutoMapper;
using Boecker.Application.Payments.Dtos;
using Boecker.Domain.Entities;
using Boecker.Domain.Execptions;
using Boecker.Domain.IRepositories;
using MediatR;

namespace Boecker.Application.Payments.Commands.CreatePaymentByInvoiceNumber;

public class CreatePaymentByInvoiceNumberCommandHandler : IRequestHandler<CreatePaymentByInvoiceNumberCommand, PaymentDto>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public CreatePaymentByInvoiceNumberCommandHandler(
        IInvoiceRepository invoiceRepository,
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<PaymentDto> Handle(CreatePaymentByInvoiceNumberCommand request, CancellationToken cancellationToken)
    {
        // 1. Look up the invoice by number.
        var invoice = await _invoiceRepository.GetByNumberAsync(request.InvoiceNumber, cancellationToken);
        if (invoice == null)
        {
            throw new NotFoundException($"Invoice with number {request.InvoiceNumber} not found.", "Invoice");
        }


        // 2. Create the Payment entity.
        var payment = new Payment
        {
            InvoiceId = invoice.InvoiceId,
            PaymentDate = request.PaymentDate,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod
        };

        // 3. Save the new Payment.
        await _paymentRepository.AddAsync(payment, cancellationToken);
        await _paymentRepository.SaveChangesAsync(cancellationToken);

        // 4. Map to PaymentDto to return.
        return _mapper.Map<PaymentDto>(payment);
    }
}
