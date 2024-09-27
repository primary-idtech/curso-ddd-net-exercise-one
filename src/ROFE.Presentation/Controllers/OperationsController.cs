using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROFE.Application.Operations.Create;
using ROFE.Presentation.ViewModels.Request;
using ROFE.Presentation.ViewModels.Response;
using System.Threading.Tasks;

namespace ROFE.Presentation.Controllers;

/// <summary>
/// Operations Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OperationsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    /// <summary>
    /// Create Monetary Operation
    /// </summary>
    /// <returns>Information resulting from the creation operation</returns>
    /// <remarks>
    /// Create a new Monetary Operation.
    /// 
    /// Sample request:
    ///
    ///     POST /api/operations/monetary
    ///     {
    ///         "userId": 1, "TODO: Para simplificar se agrega el "UserId" el cual deberia ser obtenido del token de autenticacion."
    ///         "portfolioId": 1,
    ///         "amount": 100,
    ///         "currency": "ARS",
    ///         "comment": "This is a comment associated with the monetary operation."
    ///     }
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="401">The request is not validly authenticated</response>
    /// <response code="403">The client is not authorized for using this operation</response>
    /// <response code="404">The resource was not found</response>
    [HttpPost("monetary")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MonetaryOperationResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateMonetary([FromBody] CreateMonetaryOperationRequest req)
    {
        var cmd = new CreateMonetaryCommand { 
            UserId = req.UserId,
            PortfolioId = req.PortfolioId,
            Amount = req.Amount,
            Currency = req.Currency,
            Comment = req.Comment
        };

        var operation = await this.mediator.Send(cmd);
        return this.Created($"/api/operation/{operation.Id}", new MonetaryOperationResponse(operation));
    }

    /// <summary>
    /// Create Stock Operation
    /// </summary>
    /// <returns>Information resulting from the creation operation</returns>
    /// <remarks>
    /// Create a new Stock Operation.
    /// 
    /// Sample request:
    ///
    ///     POST /api/operations/stock
    ///     {
    ///         "userId": 1, "TODO: Para simplificar se agrega el "UserId" el cual deberia ser obtenido del token de autenticacion."
    ///         "portfolioId": 1,
    ///         "amount": 100,
    ///         "currency": "ARS",
    ///         "tradeAgentId": 1,
    ///         "tradeDate": "2024-09-27T10:11:12",
    ///         "instrumentId": 1,
    ///         "quantity": 10
    ///     }
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="401">The request is not validly authenticated</response>
    /// <response code="403">The client is not authorized for using this operation</response>
    /// <response code="404">The resource was not found</response>
    [HttpPost("stock")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StockOperationResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateStock([FromBody] CreateStockOperationRequest req)
    {
        var cmd = new CreateStockCommand
        {
            UserId = req.UserId,
            PortfolioId = req.PortfolioId,
            TradeAgentId = req.TradeAgentId,
            TradeDate = req.TradeDate,
            InstrumentId = req.InstrumentId,
            Quantity = req.Quantity,
            Amount = req.Amount,
            Currency = req.Currency
        };

        var operation = await this.mediator.Send(cmd);
        return this.Created($"/api/operation/{operation.Id}", new StockOperationResponse(operation));
    }
}