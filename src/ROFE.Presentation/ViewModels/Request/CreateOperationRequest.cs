using ROFE.Domain.Models.Share;
using System.ComponentModel.DataAnnotations;

namespace ROFE.Presentation.ViewModels.Request;

/// <summary>
/// Create Operation Request Model
/// </summary>
public class CreateOperationRequest
{
    /// <summary>
    /// Identifier from User
    /// TODO: El UserId deberia ser obtenido del token de autenticacion.
    /// </summary>
    /// <example>12</example>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Identifier from Portfolio
    /// </summary>
    /// <example>45</example>
    [Required]
    public int PortfolioId { get; set; }

    /// <summary>
    /// Amount from Operation
    /// </summary>
    /// <example>10000</example>
    [Required]
    public double Amount { get; set; }
    /// <summary>
    /// Currency from Operation
    /// </summary>
    /// <example>ARS</example>
    [Required]
    public Currency Currency { get; set; }
}
