using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Entities;

public class LayoutSection(Guid id, string name, LayoutSectionProps props)
{
    private Rect m_rect = props.Rect;

    private GridSize m_size = props.Size;

    //
    
    public Guid Id => id;

    public string Name => name;

    public Rect Rect => m_rect;

    public GridSize Size => m_size;

    //

    internal void SetRect(Rect rect)
    {
        m_rect = rect;
    }

    internal void SetSize(GridSize size)
    {
        m_size = size;
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