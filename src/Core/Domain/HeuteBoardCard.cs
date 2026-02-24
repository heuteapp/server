using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteBoardCard(Guid id)
{
    private string? m_sectionId = null;

    private GridRect? m_position = null;

    private string? m_title = null;

    private bool m_isVerified = false;

    //

    public Guid Id => id;

    public string? SectionId => m_sectionId;

    public GridRect? Position => m_position;

    public string? Title => m_title;

    //

    public bool IsPlaced
    {
        get
        {
            return m_sectionId != null && m_position != null;
        }
    }

    public bool IsVerified
    {
        get
        {
            return m_isVerified;
        }
    }
    
    //

    internal void DoPlace(string sectionId, GridRect position)
    {
        ArgumentNullException.ThrowIfNull(sectionId);
        ArgumentNullException.ThrowIfNull(position);

        m_sectionId = sectionId;
        m_position = position;
        m_isVerified = false;
    }

    internal void DoUnplace()
    {
        m_sectionId = null;
        m_position = null;
        m_isVerified = false;
    }

    internal void SetTitle(string title)
    {
        ArgumentNullException.ThrowIfNull(title);

        m_title = title;
    }

    internal void DoVerify()
    {
        m_isVerified = true;
    }

    //

    internal HeuteBoardCardSnapshot ToSnapshot()
    {
        return new HeuteBoardCardSnapshot(
            Id,
            new HeuteBoardCardProps(
                SectionId,
                Position,
                Title
            )
        );
    }

    internal static HeuteBoardCard FromSnapshot(HeuteBoardCardSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return new HeuteBoardCard(snapshot.Id)
        {
            m_position = snapshot.Props.Position,
            m_sectionId = snapshot.Props.SectionId,
            m_title = snapshot.Props.Title
        };
    }
}

public sealed record HeuteBoardCardSnapshot(
    Guid Id,
    HeuteBoardCardProps Props
);

public sealed record HeuteBoardCardProps(
    string? SectionId,
    GridRect? Position,
    string? Title
);