using ROFE.Domain.Models.Operation;
using System;
using System.Text.Json.Serialization;

namespace ROFE.Presentation.ViewModels.Response;

/// <summary>
/// Operation Response.
/// </summary>
/// <param name="entity"></param>
public class OperationResponse(Operation entity)
{
    /// <summary>
    /// Identifier from Operation
    /// </summary>
    /// <example>1</example>
    [JsonPropertyOrder(1)]
    public int Id { get; set; } = entity.Id;

    /// <summary>
    /// Type of Operation
    /// </summary>
    /// <example>Monetary|Stock</example>
    [JsonPropertyOrder(2)]
    public string Type { get; set; } = entity.Type.Name;

    /// <summary>
    /// User Identifier from the Operation
    /// </summary>
    /// <example>12</example>
    [JsonPropertyOrder(3)]
    public int UserId { get; set; } = entity.UserId;

    /// <summary>
    /// Portfolio Identifier from the Operation
    /// </summary>
    /// <example>45</example>
    [JsonPropertyOrder(4)]
    public int PortfolioId { get; set; } = entity.PortfolioId;

    /// <summary>
    /// Price from the Operation
    /// </summary>
    /// <example>{"Amount": 100, "Currency": "ARS" }</example>
    [JsonPropertyOrder(5)]
    public Price Price { get; set; } = entity.Price;

    /// <summary>
    /// Created At from the Operation, Date UTC in format yyyymmddThh:mm:ss
    /// </summary>
    /// <example>2024-09-27T10:11:12</example>
    [JsonPropertyOrder(6)]
    public DateTime CreatedAt { get; set; } = entity.CreatedAt;
}
