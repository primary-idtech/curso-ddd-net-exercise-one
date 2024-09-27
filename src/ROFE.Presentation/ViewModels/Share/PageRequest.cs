using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ROFE.Presentation.ViewModels.Share;

/// <summary>
/// </summary>
public class PageRequest
{
    /// <summary>
    /// Sorting criteria in the format: property,(asc|desc). Default sort order is ascending.
    /// </summary>
    /// <example>id,desc</example>
    [FromQuery(Name = "sort")]
    public string? Sort { get; set; }

    /// <summary>
    /// Results page you want to retrieve(0..N)
    /// </summary>
    /// <example>0</example>
    [FromQuery(Name = "offset")]
    public ushort Offset { get; set; }

    /// <summary>
    /// Number of records per page. Range 1 to 200.
    /// </summary>
    /// <example>200</example>
    [Required]
    [Range(1, 200)]
    [FromQuery(Name = "limit")]
    public required ushort Limit { get; set; }
}
