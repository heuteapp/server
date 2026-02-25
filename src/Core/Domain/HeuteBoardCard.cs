using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteBoardCard(Guid id, HeuteBoardCardProps props)
{   
    private string m_title = props.Title;

    private string? m_sectionId = props.SectionId;

    private GridRect? m_position = props.Position;

    private bool m_isVerified = false;

    //

    public Guid Id => id;

    public string Title => m_title;

    public string? SectionId => m_sectionId;

    public GridRect? Position => m_position;

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
                Title,
                SectionId,
                Position
            )
        );
    }

    internal static HeuteBoardCard FromSnapshot(HeuteBoardCardSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        return new HeuteBoardCard(snapshot.Id, snapshot.Props);
    }
}

public sealed record HeuteBoardCardSnapshot(
    Guid Id,
    HeuteBoardCardProps Props
);

public sealed record HeuteBoardCardProps(
    string Title,
    string? SectionId,
    GridRect? Position
);