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

    //

    public LayoutSectionSnapshot ToSnapshot()
    {
        return new LayoutSectionSnapshot(
            Id,
            Name,
            new LayoutSectionProps(
                Rect,
                Size
            )
        );
    }

    public LayoutSectionProps ToProps()
    {
        return new LayoutSectionProps(
            Rect,
            Size
        );
    }

    public static LayoutSection FromSnapshot(LayoutSectionSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        return new LayoutSection(snapshot.Id, snapshot.Name, snapshot.Props);
    }

    public static LayoutSection FromProps(Guid id, string name, LayoutSectionProps props)
    {
        ArgumentNullException.ThrowIfNull(props);
        return new LayoutSection(id, name, props);
    }
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