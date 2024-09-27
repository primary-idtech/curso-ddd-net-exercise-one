using MediatR;

namespace ROFE.Domain.Models.Operation.DomainEvents;

public record class StockOperationCreatedEvent
(
    StockOperation Operation,
    int UserId
) : INotification;