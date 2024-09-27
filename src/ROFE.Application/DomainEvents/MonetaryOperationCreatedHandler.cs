using MediatR;
using Microsoft.Extensions.Logging;
using ROFE.Domain.Models.Operation.DomainEvents;
using ROFE.Domain.Models.Portfolio;
using System.Threading;
using System.Threading.Tasks;

namespace ROFE.Application.DomainEvents;

public class MonetaryOperationCreatedHandler(
    IPortfolioRepository repository,
    ILogger<MonetaryOperationCreatedHandler> logger) : INotificationHandler<MonetaryOperationCreatedEvent>
{
    private readonly ILogger _logger = logger;
    private readonly IPortfolioRepository repository = repository;

    public async Task Handle(MonetaryOperationCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("MonetaryOperationCreatedEvent - {OperationId}", domainEvent.Operation.Id);

        var portfolioId = domainEvent.Operation.PortfolioId;

        var portfolio = await this.repository.GetByIdWithIncludesAsync(portfolioId);
        if (portfolio == null)
        {
            portfolio = new Portfolio(portfolioId);
            portfolio.AddOperation(domainEvent.Operation);
            await this.repository.AddAsync(portfolio);
        }
        else
        {
            portfolio.AddOperation(domainEvent.Operation);
            this.repository.Update(portfolio);
        }

        await this.repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
