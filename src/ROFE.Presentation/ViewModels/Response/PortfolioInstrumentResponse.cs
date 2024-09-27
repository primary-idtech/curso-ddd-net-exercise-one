using ROFE.Domain.Models.Portfolio;

namespace ROFE.Presentation.ViewModels.Response;

/// <summary>
/// PortfolioInstrument Response.
/// </summary>
/// <param name="entity"></param>
public class PortfolioInstrumentResponse(PortfolioInstrument entity)
{
    /// <summary>
    /// Instrument Identifier.
    /// </summary>
    /// <example>5</example>
    public int InstrumentId { get; set; } = entity.InstrumentId;

    /// <summary>
    /// Average Purchase Price.
    /// </summary>
    /// <example>{"Quantity": 10, "Amount": 100, "Currency": "ARS"}</example>
    public APP AveragePurchasePrice { get; set; } = entity.AveragePurchasePrice;
}
