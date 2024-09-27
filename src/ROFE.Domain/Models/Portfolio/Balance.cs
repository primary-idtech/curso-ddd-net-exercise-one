using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Share;
using System.Collections.Generic;

namespace ROFE.Domain.Models.Portfolio;

public class Balance : ValueObject
{
    public double Amount { get; private set; }
    public Currency Currency { get; private set; }
    
    public Balance() { }

    public Balance(double amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public Balance Sum(double amount, Currency currency)
    {
        //TODO: Logica para trabajar con diferentes monedas, para el ejemplo se deja la misma moneda ARS.
        this.Currency = Currency.ARS;

        return new Balance(Amount + amount, Currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}

