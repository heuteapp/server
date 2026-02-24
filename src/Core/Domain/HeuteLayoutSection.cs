using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteLayoutSection(string id)
{
    private Rect? m_rect = null;

    private GridSize? m_size = null;

    //
    
    public string Id => id;

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

    public HeuteLayoutSectionSnapshot ToSnapshot()
    {
        return new HeuteLayoutSectionSnapshot(
            Id,
            Rect,
            Size
        );
    }

    public static HeuteLayoutSection FromSnapshot(HeuteLayoutSectionSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        var section = new HeuteLayoutSection(snapshot.Id)
        {
            m_rect = snapshot.Rect,
            m_size = snapshot.Size
        };
        
        return section;
    }
}

public sealed record HeuteLayoutSectionSnapshot(
    string Id,
    Rect? Rect,
    GridSize? Size
);