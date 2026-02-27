using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Entities;

public class LayoutSection(Guid id, string name, LayoutSectionProps props)
{
    public Guid Id { get; private set; } = id;

    public string Name { get; private set; } = name;

    public Rect Rect { get; internal set; } = props.Rect;

    public GridSize Size { get; internal set; } = props.Size;
}

public sealed record LayoutSectionProps(
    Rect Rect,
    GridSize Size
);