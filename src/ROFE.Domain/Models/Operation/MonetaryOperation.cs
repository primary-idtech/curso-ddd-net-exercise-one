using ROFE.Domain.Models.Operation.DomainEvents;

namespace ROFE.Domain.Models.Operation;

public class MonetaryOperation : Operation
{
    private static new readonly TypeOperation Type= TypeOperation.Monetary;
    public string Comment { get; private set; }

    public MonetaryOperation(int userId) : base(Type, userId)
    {
        AddDomainEvent(new MonetaryOperationCreatedEvent(this, userId));
    }

    public void SetComment(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
            throw new BusinessException("The comment is invalid");

        this.Comment = comment;
    }
}
