using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Entities;

public class LayoutSection
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public Rect Rect { get; internal set; } = null!;

    public GridSize Size { get; internal set; } = null!;

    //

    private LayoutSection() { }

    private LayoutSection(Guid id, string name, LayoutSectionProps props)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(props);

        Id = id;
        Name = name;
        Rect = props.Rect;
        Size = props.Size;
    }

    public static LayoutSection Create(Guid id, string name, LayoutSectionProps props)
        => new(id, name, props);
}

public sealed record LayoutSectionSnapshot(
    Guid Id,
    string Name,
    LayoutSectionProps Props
);

public sealed record LayoutSectionProps(
    Rect Rect,
    GridSize Size
);