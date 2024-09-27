using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Share;
using System.Collections.Generic;

namespace ROFE.Domain.Models.Portfolio;

/// <summary>
/// Average Purchase Price.
/// </summary>
public class APP : ValueObject
{
    public int Quantity { get; private set; }
    public double Amount { get; private set; }
    public Currency Currency { get; private set; }
    
    private APP() { }

    public APP(int quantity, double amount, Currency currency)
    {
        if (quantity < 0)
            throw new BusinessException("The resulting quantity for instrument must be greater than or equal to zero.");

        if (amount <= 0)
            throw new BusinessException("The amount must be greater than zero.");

        Quantity = quantity;
        Amount = amount;
        Currency = currency;
    }

    public APP Calculate(int quantity, double amount, Currency currency)
    {
        if (quantity == 0)
            throw new BusinessException("The quantity must be not zero.");

        // totalQuantity -> Total shares = Σ(number of shares)
        var totalQuantity = Quantity + quantity;

        if (quantity > 0)
        {
            // totalAmount -> Total purchase price = Σ(share price * number of shares)
            // avgAmount -> Average price = total purchase price / total shares
            var totalAmount = Amount * Quantity + amount * quantity;
            var avgAmount = totalAmount / totalQuantity;

            return new APP(totalQuantity, avgAmount, currency);
        }

        return new APP(totalQuantity, Amount, currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Quantity;
        yield return Amount;
        yield return Currency;
    }
}

