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
            Rect,
            Size
        );
    }

    internal static HeuteLayoutSection FromSnapshot(HeuteLayoutSectionSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        var section = new HeuteLayoutSection(snapshot.Id, snapshot.Name)
        {
            m_rect = snapshot.Rect,
            m_size = snapshot.Size
        };

        return section;
    }
}

public sealed record HeuteLayoutSectionSnapshot(
    Guid Id,
    string Name,
    Rect? Rect,
    GridSize? Size
);