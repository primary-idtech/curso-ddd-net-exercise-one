using MediatR;
using Microsoft.Extensions.Logging;
using ROFE.Domain.Abstractions;
using ROFE.Domain.Models.Operation;
using ROFE.Domain.Models.Share;
using System.Threading;
using System.Threading.Tasks;

namespace ROFE.Application.Operations.Create;

public record CreateMonetaryCommand : IRequest<MonetaryOperation>
{
    public int UserId { get; set; }
    public int PortfolioId { get; set; }
    public double Amount { get; set; }
    public Currency Currency { get; set; }
    public string Comment { get; set; }
}

public class CreateMonetaryHandler(
    IRepository<MonetaryOperation> repository,
    ILogger<CreateMonetaryCommand> logger) : IRequestHandler<CreateMonetaryCommand, MonetaryOperation>
{
    private readonly IRepository<MonetaryOperation> repository = repository;
    private readonly ILogger<CreateMonetaryCommand> logger = logger;

    public async Task<MonetaryOperation> Handle(CreateMonetaryCommand cmd, CancellationToken cancellationToken)
    {
        this.logger.LogDebug("call Monetary Operation CreateCommand");

        var operation = new MonetaryOperation(cmd.UserId);
        operation.SetPrice(new Price(cmd.Amount, cmd.Currency));
        operation.SetComment(cmd.Comment);
        operation.SetPortfolioId(cmd.PortfolioId);

        await this.repository.AddAsync(operation);
        await this.repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return operation;
    }
}
