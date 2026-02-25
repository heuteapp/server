using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteLayoutSection(Guid id, string name, HeuteLayoutSectionProps props)
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

    public HeuteLayoutSectionSnapshot ToSnapshot()
    {
        return new HeuteLayoutSectionSnapshot(
            Id,
            Name,
            new HeuteLayoutSectionProps(
                Rect,
                Size
            )
        );
    }

    public HeuteLayoutSectionProps ToProps()
    {
        return new HeuteLayoutSectionProps(
            Rect,
            Size
        );
    }

    public static HeuteLayoutSection FromSnapshot(HeuteLayoutSectionSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        return new HeuteLayoutSection(snapshot.Id, snapshot.Name, snapshot.Props);
    }

    public static HeuteLayoutSection FromProps(Guid id, string name, HeuteLayoutSectionProps props)
    {
        ArgumentNullException.ThrowIfNull(props);
        return new HeuteLayoutSection(id, name, props);
    }
}

public sealed record HeuteLayoutSectionSnapshot(
    Guid Id,
    string Name,
    HeuteLayoutSectionProps Props
);

public sealed record HeuteLayoutSectionProps(
    Rect Rect,
    GridSize Size
);