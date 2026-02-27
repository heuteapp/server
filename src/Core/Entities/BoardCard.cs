using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Entities;

public class BoardCard(Guid id, BoardCardProps props)
{   
    private string m_title = props.Title;

    private Guid? m_sectionId = props.SectionId;

    private GridRect? m_position = props.Position;

    private bool m_isVerified = false;

    //

    public Guid Id => id;

    public string Title => m_title;

    public Guid? SectionId => m_sectionId;

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

    internal void SetTitle(string title)
    {
        ArgumentNullException.ThrowIfNull(title);

        m_title = title;
    }

    internal void DoPlace(Guid sectionId, GridRect position)
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

    internal void DoVerify()
    {
        m_isVerified = true;
    }

    //

    public BoardCardSnapshot ToSnapshot()
    {
        return new BoardCardSnapshot(
            Id,
            new BoardCardProps(
                Title,
                SectionId,
                Position
            )
        );
    }

    public BoardCardProps ToProps()
    {
        return new BoardCardProps(
            Title,
            SectionId,
            Position
        );
    }

    public static BoardCard FromSnapshot(BoardCardSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        return new BoardCard(snapshot.Id, snapshot.Props);
    }

    public static BoardCard FromProps(Guid id, BoardCardProps props)
    {
        ArgumentNullException.ThrowIfNull(props);
        return new BoardCard(id, props);
    }
}

public sealed record BoardCardSnapshot(
    Guid Id,
    BoardCardProps Props
);

public sealed record BoardCardProps(
    string Title,
    Guid? SectionId,
    GridRect? Position
);