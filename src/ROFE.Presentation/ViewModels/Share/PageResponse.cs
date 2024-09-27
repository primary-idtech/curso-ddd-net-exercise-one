using System.Collections.Generic;

namespace ROFE.Presentation.ViewModels.Share;

/// <summary>
/// Page response.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageResponse<T> where T : class
{
    /// <summary>
    /// Total result.
    /// </summary>
    /// <example>1</example>
    public int Total { get; set; }
    /// <summary>
    /// Page result (0..N).
    /// </summary>
    /// <example>0</example>
    public uint Offset { get; set; }
    /// <summary>
    /// Limit per page.
    /// </summary>
    /// <example>200</example>
    public ushort Limit { get; set; }
    /// <summary>
    /// Items.
    /// </summary>
    public IEnumerable<T>? Items { get; set; }
}
