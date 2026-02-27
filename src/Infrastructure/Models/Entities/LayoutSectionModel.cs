using HeuteApp.Core.ValueObjects;
using HeuteApp.Infrastructure.Models.Aggregates;

namespace HeuteApp.Infrastructure.Models.Entities;

public class LayoutSectionModel
{
    public Guid Id { get; set; }

    public Guid LayoutId { get; set; }

    public HeuteLayoutModel Layout { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public Rect Rect { get; set; } = null!;

    public GridSize Size { get; set; } = null!;
}