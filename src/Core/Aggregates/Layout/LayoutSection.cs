using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Aggregates.Layout;

public class LayoutSection
{
    protected LayoutSection() { }

    protected LayoutSection(Guid id, string name, LayoutSectionProps props)
    {
        Id = id;
        Name = name;
        Rect = props.Rect;
        Size = props.Size;
    }

    public static LayoutSection Create(Guid id, string name, LayoutSectionProps props)
    {
        ArgumentNullException.ThrowIfNull(props);
        return new LayoutSection(id, name, props);
    }

    //

    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public Rect Rect { get; internal set; } = null!;

    public GridSize Size { get; internal set; } = null!;
}

public sealed record LayoutSectionProps(
    Rect Rect,
    GridSize Size
);