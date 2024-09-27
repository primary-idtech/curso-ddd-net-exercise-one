using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Share;
using System.Collections.Generic;

namespace ROFE.Domain.Models.Operation;

public class Price : ValueObject
{
    public double Amount { get; private set; }
    public Currency Currency { get; private set; }

    public Price() { }

    public Price(double amount, Currency currency)
    {
        if (amount <= 0)
            throw new BusinessException("The amount must be greater than zero");

        this.Amount = amount;
        this.Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}

