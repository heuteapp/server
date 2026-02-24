using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteLayoutSection(Guid id, string name)
{
    private Rect? m_rect = null;

    private GridSize? m_size = null;

    //
    
    public Guid Id => id;

    public string Name => name;

    public Rect? Rect => m_rect;

    public GridSize? Size => m_size;

    //

    internal void SetRect(Rect? rect)
    {
        m_rect = rect;
    }

    internal void SetSize(GridSize? size)
    {
        m_size = size;
    }

    //

    internal HeuteLayoutSectionSnapshot ToSnapshot()
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

    internal static HeuteLayoutSection FromSnapshot(HeuteLayoutSectionSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        var section = new HeuteLayoutSection(snapshot.Id, snapshot.Name)
        {
            m_rect = snapshot.Props.Rect,
            m_size = snapshot.Props.Size
        };

        return section;
    }
}

public sealed record HeuteLayoutSectionSnapshot(
    Guid Id,
    string Name,
    HeuteLayoutSectionProps Props
);

public sealed record HeuteLayoutSectionProps(
    Rect? Rect,
    GridSize? Size
);