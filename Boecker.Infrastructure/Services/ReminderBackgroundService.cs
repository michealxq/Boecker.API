
using Boecker.Application.Common.Interfaces;
using Boecker.Domain.Events;
using Boecker.Domain.IRepositories;
using Boecker.Domain.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Boecker.Infrastructure.Services;

public class ReminderBackgroundService : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<ReminderBackgroundService> _logger;
    private readonly ReminderSettings _settings;

    public ReminderBackgroundService(IServiceProvider provider, IOptions<ReminderSettings> settings, ILogger<ReminderBackgroundService> logger)
    {
        _provider = provider;
        _logger = logger;
        _settings = settings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _provider.CreateScope();
                var scheduleRepo = scope.ServiceProvider.GetRequiredService<IServiceScheduleRepository>();
                var invoiceRepo = scope.ServiceProvider.GetRequiredService<IInvoiceRepository>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await SendServiceReminders(scheduleRepo, mediator, stoppingToken);
                await SendPaymentReminders(invoiceRepo, mediator, stoppingToken);

                _logger.LogInformation("✅ Reminder job completed at {Time}", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error in ReminderBackgroundService");
            }

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Run daily
        }
    }

    private async Task SendServiceReminders(IServiceScheduleRepository scheduleRepo, IMediator mediator, CancellationToken token)
    {
        var targetDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_settings.ServiceDaysBefore));
        var schedules = await scheduleRepo.GetScheduledByDateAsync(targetDate, token);

        foreach (var schedule in schedules)
        {
            var email = schedule.Contract.Customer.Email;
            if (string.IsNullOrWhiteSpace(email)) continue;

            await mediator.Publish(new UpcomingServiceReminderEvent(schedule.ServiceScheduleId, schedule.DateScheduled, email)
            , token);

            _logger.LogInformation("📨 Published UpcomingServiceReminderEvent for {Email}", email);
        }
    }

    private async Task SendPaymentReminders(IInvoiceRepository invoiceRepo, IMediator mediator, CancellationToken token)
    {
        var targetDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_settings.PaymentDaysBefore));
        var invoices = await invoiceRepo.GetPendingDueByDateAsync(targetDate, token);

        foreach (var invoice in invoices)
        {
            if (string.IsNullOrWhiteSpace(invoice.Client.Email)) continue;

            await mediator.Publish(new PaymentReminderEvent(invoice)
            , token);

            _logger.LogInformation("📨 Published PaymentReminderEvent for Invoice #{Invoice}", invoice.InvoiceNumber);
        }
    }
}
