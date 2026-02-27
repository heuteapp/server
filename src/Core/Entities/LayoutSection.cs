using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Entities;

public class LayoutSection(Guid id, string name, LayoutSectionProps props)
{
    protected LayoutSection() : this(Guid.Empty, string.Empty, new LayoutSectionProps(new Rect(0, 0, 0, 0), new GridSize(0, 0)))
    {
        
    }

    public Guid Id { get; private set; } = id;

    public string Name { get; private set; } = name;

    public Rect Rect { get; internal set; } = props.Rect;

    public GridSize Size { get; internal set; } = props.Size;
}

public sealed record LayoutSectionProps(
    Rect Rect,
    GridSize Size
);