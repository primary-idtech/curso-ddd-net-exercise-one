using ROFE.Application.Shared.DTOs;
using ROFE.Domain.Models.Portfolio;
using ROFE.Presentation.ViewModels.Share;
using System.Linq;

namespace ROFE.Presentation.ViewModels.Response;

/// <summary>
/// Information result for search Portfolios.
/// </summary>
public class PortfolioPageResponse : PageResponse<PortfolioResponse>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="dto">Page with portfolio items</param>
    public PortfolioPageResponse(PageDto<Portfolio> dto)
    {
        Items = dto.Items.Select(x => new PortfolioResponse(x));
        Offset = dto.Offset;
        Total = dto.Total;
        Limit = dto.Limit;
    }
}
