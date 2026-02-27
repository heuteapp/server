using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Entities;

public class BoardCard
{   
    public Guid Id { get;  private set; }

    public string? Title { get; private set; }

    public Guid? SectionId { get; private set; }

    public GridRect? Position { get; private set; }

    public bool IsPlaced
    {
        get
        {
            return SectionId != null && Position != null;
        }
    }

    public bool IsVerified { get; private set; } = false;

    //

    private BoardCard() { } // EF için

    private BoardCard(Guid id, BoardCardProps props)
    {
        Id = id;
        Title = props.Title;
        SectionId = props.SectionId;
        Position = props.Position;
        IsVerified = false;
    }
    
    public static BoardCard Create(Guid id, BoardCardProps props)
        => new(id, props);

    //

    internal void DoPlace(Guid sectionId, GridRect position)
    {
        ArgumentNullException.ThrowIfNull(position);

        SectionId = sectionId;
        Position = position;
        IsVerified = false;
    }

    internal void DoUnplace()
    {
        SectionId = null;
        Position = null;
        IsVerified = false;
    }

    internal void DoVerify()
    {
        IsVerified = true;
    }
}

public sealed record BoardCardSnapshot(
    Guid Id,
    BoardCardProps Props
);

public sealed record BoardCardProps(
    string? Title,
    Guid? SectionId,
    GridRect? Position
);