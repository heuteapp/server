using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Infrastructure.Persistence.Entities;

public class LayoutSectionEntity
{
    public Guid Id { get; set; }

    public Guid LayoutId { get; set; }

    public LayoutEntity Layout { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public Rect Rect { get; set; } = null!;

    public GridSize Size { get; set; } = null!;
}