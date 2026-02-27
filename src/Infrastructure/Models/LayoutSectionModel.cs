using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Infrastructure.Models;

public class LayoutSectionModel
{
    public Guid Id { get; set; }

    public Guid LayoutId { get; set; }

    public HeuteLayoutModel Layout { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public Rect Rect { get; set; } = null!;

    public GridSize Size { get; set; } = null!;
}