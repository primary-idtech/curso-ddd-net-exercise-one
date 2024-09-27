using MediatR;

namespace ROFE.Domain.Models.Operation.DomainEvents;

public record class MonetaryOperationCreatedEvent
(
    MonetaryOperation Operation,
    int UserId
) : INotification;