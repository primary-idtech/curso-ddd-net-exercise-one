using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Operation;
using System.Collections.Generic;

namespace ROFE.Domain.Models.Portfolio;

public partial class PortfolioInstrument : Entity
{
    public int InstrumentId { get; private set; }
    public APP AveragePurchasePrice { get; private set; }
    public ICollection<PortfolioInstrumentHistory> Histories { get; private set; }

    private PortfolioInstrument() { }

    public PortfolioInstrument(int instrumentId)
    {
        if (instrumentId <= 0)
            throw new BusinessException("The instrument id is invalid");

        this.InstrumentId = instrumentId;
    }

    public void AddHistory()
    {
        this.Histories ??= [];
        this.Histories.Add(new PortfolioInstrumentHistory(this));
    }

    public void SetAveragePurchasePrice(StockOperation operation)
    {
        this.AveragePurchasePrice = this.AveragePurchasePrice != null
            ? this.AveragePurchasePrice.Calculate(operation.Quantity, operation.Price.Amount, operation.Price.Currency)
            : new APP(operation.Quantity, operation.Price.Amount, operation.Price.Currency);
    }
}
