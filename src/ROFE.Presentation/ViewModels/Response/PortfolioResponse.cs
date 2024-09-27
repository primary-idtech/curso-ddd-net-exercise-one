using ROFE.Domain.Models.Portfolio;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ROFE.Presentation.ViewModels.Response;

/// <summary>
/// Portfolio Response.
/// </summary>
/// <remarks>
/// </remarks>
public class PortfolioResponse(Portfolio entity)
{
    /// <summary>
    /// Identifier of the Portfolio
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; } = entity.Id;
    /// <summary>
    /// Balance of the Portfolio
    /// </summary>
    /// <example>{"Amount": 100, "Currency": "ARS"}</example>
    public Balance Balance { get; set; } = entity.Balance;

    /// <summary>
    /// Instruments of the Portfolio
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<PortfolioInstrumentResponse>? Instruments { get; set; } = GetValidInstruments(entity?.Instruments);


    private static List<PortfolioInstrumentResponse>? GetValidInstruments(ICollection<PortfolioInstrument>? instruments)
    {
        if (instruments == null) return null;

        var validInstruments = new List<PortfolioInstrumentResponse>();

        foreach (var instrument in instruments)
            if (instrument?.AveragePurchasePrice?.Quantity > 0)
                validInstruments.Add(new PortfolioInstrumentResponse(instrument));

        return validInstruments.Count > 0 ? validInstruments : null;
    }
}
