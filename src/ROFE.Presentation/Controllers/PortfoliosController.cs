using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROFE.Application.Portfolios.FindAll;
using ROFE.Application.Portfolios.FindOne;
using ROFE.Presentation.ViewModels.Request;
using ROFE.Presentation.ViewModels.Response;
using System.Threading.Tasks;

namespace ROFE.Presentation.Controllers;


/// <summary>
/// Portfolio Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PortfoliosController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    /// <summary>
    /// Find All
    /// </summary>
    /// <remarks>
    /// Returns paginated Portfolios results.<br />
    /// The results are sorted by the field and direction specified in the sort parameter.<br />
    /// The offset parameter specifies the starting point to return results.<br />
    /// The limit parameter specifies the maximum number of results to return.<br />
    /// The default values are offset=0 and limit=200.<br />
    ///
    /// Sample request:
    ///
    ///     GET /api/portfolios/findAll?sort=id,desc&amp;offset=0&amp;limit=200
    /// </remarks>
    /// <response code="200">Request successful</response>
    /// <response code="401">The request is not validly authenticated</response>
    /// <response code="403">The client is not authorized for using this operation</response>
    /// <response code="404">The resource was not found</response>
    [HttpGet("findAll")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortfolioPageResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 5)]
    public async Task<IActionResult> GetAll([FromQuery] PortfolioPageRequest req)
    {
        var query = new FindAllQuery()
        {
            Limit = req.Limit,
            Offset = req.Offset,
            Sort = req.Sort
        };
        var pageDto = await this.mediator.Send(query);

        return this.Ok(new PortfolioPageResponse(pageDto));
    }

    /// <summary>
    /// Get by ID
    /// </summary>
    /// <param name="id" example="1">Identifier from Portfolio</param>
    /// <returns>Information result for one Portfolio</returns>
    /// <remarks>
    /// Returns one Portfolio according to ID.
    /// 
    /// Sample request:
    ///
    ///     GET /api/portfolios/1
    /// </remarks>
    /// <response code="200">Request successful</response>
    /// <response code="401">The request is not validly authenticated</response>
    /// <response code="403">The client is not authorized for using this operation</response>
    /// <response code="404">The resource was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortfolioResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 5)]
    public async Task<IActionResult> Get(int id)
    {
        var query = new FindOneByIdQuery() { Id = id };
        var entity = await this.mediator.Send(query);
        if (entity == null) return this.NotFound();

        return this.Ok(new PortfolioResponse(entity));
    }
}
