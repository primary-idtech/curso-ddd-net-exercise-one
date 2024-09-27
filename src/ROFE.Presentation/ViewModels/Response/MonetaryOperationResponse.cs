using ROFE.Domain.Models.Operation;
using System.Text.Json.Serialization;

namespace ROFE.Presentation.ViewModels.Response;

/// <summary>
/// Monetary Operation Response.
/// </summary>
/// <param name="entity"></param>
public class MonetaryOperationResponse(MonetaryOperation entity) : OperationResponse(entity)
{
    /// <summary>
    /// Comment
    /// </summary>
    /// <example>This is a comment associated with the monetary operation.</example>
    [JsonPropertyOrder(7)]
    public string Comment { get; set; } = entity.Comment;
}
