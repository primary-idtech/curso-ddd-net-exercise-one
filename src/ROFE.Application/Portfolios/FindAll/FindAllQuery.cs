using MediatR;
using Microsoft.Extensions.Logging;
using ROFE.Application.Shared.DTOs;
using ROFE.Domain.Models.Portfolio;
using System.Threading;
using System.Threading.Tasks;

namespace ROFE.Application.Portfolios.FindAll;

public class FindAllQuery : PageQueryDto, IRequest<PageDto<Portfolio>>
{
}

public class FindAllHandler(
    IPortfolioRepository repository,
    ILogger<FindAllQuery> logger) : IRequestHandler<FindAllQuery, PageDto<Portfolio>>
{
    private readonly IPortfolioRepository repository = repository;
    private readonly ILogger<FindAllQuery> logger = logger;

    public async Task<PageDto<Portfolio>> Handle(FindAllQuery query, CancellationToken cancellationToken)
    {
        this.logger.LogDebug("call Portfolio FindAllQuery");

        var result = new PageDto<Portfolio>
        {
            Total = await this.repository.CountAsync(),
            Limit = query.Limit,
            Offset = query.Offset,
            Items = await this.repository.GetPagedAsync(query.Offset, query.Limit)
        };

        return result;
    }
}
