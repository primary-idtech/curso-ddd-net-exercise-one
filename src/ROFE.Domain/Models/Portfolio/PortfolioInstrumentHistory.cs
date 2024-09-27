using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Share;
using System;

namespace ROFE.Domain.Models.Portfolio;

public partial class PortfolioInstrumentHistory : Entity
{
    public int InstrumentId { get; private set; }
    public int Quantity { get; private set; }
    public double Amount { get; private set; }
    public Currency Currency { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private PortfolioInstrumentHistory() { }
    
    public PortfolioInstrumentHistory(PortfolioInstrument old)
    {
        InstrumentId = old.InstrumentId;
        Quantity = old.AveragePurchasePrice.Quantity;
        Amount = old.AveragePurchasePrice.Amount;
        Currency = old.AveragePurchasePrice.Currency;
        CreatedAt = DateTime.UtcNow;
    }    
}
