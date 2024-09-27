using ROFE.Domain.Models.Operation;
using System;
using System.Text.Json.Serialization;

namespace ROFE.Presentation.ViewModels.Response;

/// <summary>
/// Stock Operation Response.
/// </summary>
/// <param name="entity"></param>
public class StockOperationResponse(StockOperation entity) : OperationResponse(entity)
{
    /// <summary>
    /// Trade Agent Identifier from the Operation
    /// </summary>
    /// <example>14</example>
    [JsonPropertyOrder(7)]
    public int TradeAgentId { get; set; } = entity.TradeAgentId;
    /// <summary>
    /// Trade Date UTC of the Operation, in format YYYYMMDD
    /// </summary>
    /// <example>20240927</example>
    [JsonPropertyOrder(8)]
    public DateTime TradeDate { get; set; } = entity.TradeDate;
    /// <summary>
    /// Instrument Identifier from the Operation
    /// </summary>
    /// <example>5</example>
    [JsonPropertyOrder(9)]
    public int InstrumentId { get; set; } = entity.InstrumentId;
    /// <summary>
    /// Quantity of the Operation
    /// </summary>
    /// <example>10</example>
    [JsonPropertyOrder(10)]
    public int Quantity { get; set; } = entity.Quantity;
    /// <summary>
    /// Price from the Operation
    /// </summary>
    /// <example>{"Amount": 100, "Currency": "ARS" }</example>
    [JsonPropertyOrder(11)]
    public Price Price { get; set; } = entity.Price;
}
