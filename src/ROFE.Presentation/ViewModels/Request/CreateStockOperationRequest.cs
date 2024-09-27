using System;

namespace ROFE.Presentation.ViewModels.Request;

/// <summary>
/// Create Stock Operation Request Model
/// </summary>
public class CreateStockOperationRequest : CreateOperationRequest
{
    /// <summary>
    /// Trade Agent Identifier from the Operation
    /// </summary>
    /// <example>14</example>
    public int TradeAgentId { get; set; }
    /// <summary>
    /// Trade Date UTC of the Operation, in format yyyymmddThh:mm:ss
    /// </summary>
    /// <example>2024-09-27T10:11:12</example>
    public DateTime TradeDate { get; set; }
    /// <summary>
    /// Instrument Identifier from the Operation
    /// </summary>
    /// <example>5</example>
    public int InstrumentId { get; set; }
    /// <summary>
    /// Quantity of the Operation. Must be greater than zero for buy operations and less than zero for sell operations.
    /// </summary>
    /// <example>10</example>
    public int Quantity { get; set; }
}
