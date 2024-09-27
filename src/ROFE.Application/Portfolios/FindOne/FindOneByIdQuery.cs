using MediatR;
using Microsoft.Extensions.Logging;
using ROFE.Domain.Models.Portfolio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ROFE.Application.Portfolios.FindOne;

public record FindOneByIdQuery : IRequest<Portfolio>
{
    public int Id { get; set; }
}

public class FindOneByIdHandler(
    IPortfolioRepository repository,
    ILogger<FindOneByIdQuery> logger): IRequestHandler<FindOneByIdQuery, Portfolio>
{
    private readonly IPortfolioRepository repository = repository;
    private readonly ILogger<FindOneByIdQuery> logger = logger;

    public Task<Portfolio> Handle(FindOneByIdQuery query, CancellationToken cancellationToken)
    {
        this.logger.LogDebug("call Portfolio FindOneQuery by Id");

        if (query.Id <= 0)
            throw new ArgumentException("Id is required");

        return this.repository.GetByIdWithIncludesAsync(query.Id);
    }
}
