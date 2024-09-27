using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Operation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ROFE.Domain.Models.Portfolio;

public partial class Portfolio : Entity, IAggregateRoot
{
    public Balance Balance { get; private set; }
    public ICollection<PortfolioInstrument> Instruments { get; private set; }

    private Portfolio() { }

    public Portfolio(int id)
    {
        if (id <= 0)
            throw new BusinessException("The portfolio id is invalid");

        this.Id = id;
        this.Balance = new Balance();
    }

    public void AddOperation(MonetaryOperation operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        this.Balance = this.Balance.Sum(operation.Price.Amount, operation.Price.Currency);

        if (this.Balance.Amount < 0)
            throw new BusinessException("The resultant balance of the Portfolio cannot be negative.");
    }

    public void AddOperation(StockOperation operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        this.Instruments ??= [];

        var total = operation.Price.Amount * operation.Quantity;

        this.Balance = this.Balance.Sum(-total, operation.Price.Currency);

        if (this.Balance.Amount < 0)
            throw new BusinessException("The resultant balance of the Portfolio cannot be negative.");

        if (this.Instruments.Any(x => x.InstrumentId == operation.InstrumentId))
        {
            var instrument = this.Instruments.First(x => x.InstrumentId == operation.InstrumentId);
            instrument.AddHistory();
            instrument.SetAveragePurchasePrice(operation);
        }
        else
        {
            var instrument = new PortfolioInstrument(operation.InstrumentId);
            instrument.SetAveragePurchasePrice(operation);

            this.Instruments.Add(instrument);
        }
    }

    public void CalculateProfit(DateTime from, DateTime to)
    {
        //TODO: Incluir la logica para calcular el rendiemiento.
    }
}
