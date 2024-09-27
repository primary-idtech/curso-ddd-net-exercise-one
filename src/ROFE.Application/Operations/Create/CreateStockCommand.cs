using MediatR;
using Microsoft.Extensions.Logging;
using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Operation;
using ROFE.Domain.Models.Share;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ROFE.Application.Operations.Create;


public record CreateStockCommand : IRequest<StockOperation>
{
    public int UserId { get; set; }
    public int PortfolioId { get; set; }
    public int TradeAgentId { get; set; }
    public DateTime TradeDate { get; set; }
    public int InstrumentId { get; set; }
    public int Quantity { get; set; }
    public double Amount { get; set; }
    public Currency Currency { get; set; }
}

public class CreateStockHandler(
    IRepository<StockOperation> repository,
    ILogger<CreateStockCommand> logger) : IRequestHandler<CreateStockCommand, StockOperation>
{
    private readonly IRepository<StockOperation> repository = repository;
    private readonly ILogger<CreateStockCommand> logger = logger;

    public async Task<StockOperation> Handle(CreateStockCommand cmd, CancellationToken cancellationToken)
    {
        this.logger.LogDebug("call Stock Operation CreateCommand");

        var operation = new StockOperation(cmd.UserId);
        operation.SetPortfolioId(cmd.PortfolioId);
        operation.SetTradeAgentId(cmd.TradeAgentId);
        operation.SetTradeDate(cmd.TradeDate);
        operation.SetInstrumentId(cmd.InstrumentId);
        operation.SetQuantity(cmd.Quantity);
        operation.SetPrice(new Price(cmd.Amount, cmd.Currency));

        await this.repository.AddAsync(operation);
        await this.repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return operation;
    }
}

