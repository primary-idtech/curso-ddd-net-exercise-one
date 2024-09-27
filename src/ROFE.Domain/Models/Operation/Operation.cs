using ROFE.Domain.Abstractions;
using System;

namespace ROFE.Domain.Models.Operation;
public abstract class Operation : Entity, IAggregateRoot
{
    public TypeOperation Type { get; private set; }
    public int UserId { get; private set; }
    public int PortfolioId { get; private set; }
    public Price Price { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Operation() { }

    public Operation(TypeOperation type, int userId)
    {
        if (userId <= 0)
            throw new BusinessException("The user id is invalid");

        this.Type = type ?? throw new BusinessException("The type is invalid");
        this.UserId = userId;
        this.CreatedAt = DateTime.UtcNow;
    }

    public void SetPortfolioId(int portfolioId)
    {
        if (portfolioId <= 0)
            throw new BusinessException("The portfolio id is invalid");

        this.PortfolioId = portfolioId;
    }

    public void SetPrice(Price price)
    {
        this.Price = price ?? throw new BusinessException("The price is invalid");
    }
}
