namespace ROFE.Presentation.ViewModels.Request;

/// <summary>
/// Create Monetary Operation Request Model
/// </summary>
public class CreateMonetaryOperationRequest : CreateOperationRequest
{
    /// <summary>
    /// Comment
    /// </summary>
    /// <example>This is a comment associated with the monetary operation.</example>
    public string? Comment { get; set; }
}
