namespace ROFE.Application.Shared.DTOs;

public class PageQueryDto
{
    public ushort Offset { get; set; }
    public ushort Limit { get; set; }
    public string Sort { get; set; }
}
