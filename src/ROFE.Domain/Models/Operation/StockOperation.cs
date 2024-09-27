using ROFE.Domain.Models.Operation.DomainEvents;
using System;

namespace ROFE.Domain.Models.Operation;

public class StockOperation : Operation
{
    private static new readonly TypeOperation Type = TypeOperation.Stock;
    
    public int TradeAgentId { get; private set; }
    public int InstrumentId { get; private set; }
    public DateTime TradeDate { get; private set; }
    public DateTime SettlementDate { get; private set; }
    public int Quantity { get; private set; }

    public StockOperation(int userId) : base(Type, userId)
    {
        AddDomainEvent(new StockOperationCreatedEvent(this, userId));
    }

    public void SetTradeAgentId(int tradeAgentId)
    {
        if (tradeAgentId <= 0)
            throw new BusinessException("The trade agent id is invalid");

        TradeAgentId = tradeAgentId;
    }

    public void SetInstrumentId(int instrumentId)
    {
        if (instrumentId <= 0)
            throw new BusinessException("The instrument id is invalid");

        this.InstrumentId = instrumentId;
    }

    public void SetTradeDate(DateTime tradeDate)
    {
        if (tradeDate <= DateTime.UtcNow.AddDays(-1))
            throw new BusinessException("The trade date must be today");

        this.TradeDate = tradeDate;
    }

    public void SetSettlementDate(DateTime settlementDate)
    {
        if (settlementDate <= DateTime.UtcNow.AddDays(-1))
            throw new BusinessException("The settlement date must be today");

        this.SettlementDate = settlementDate;
    }

    public void SetQuantity(int quantity)
    {
        if (quantity == 0)
            throw new BusinessException("The quantity must be not zero");

        Quantity = quantity;
    }
}
