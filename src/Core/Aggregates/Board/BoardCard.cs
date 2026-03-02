using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Aggregates.Board;

public class BoardCard
{
    protected BoardCard() { }

    protected BoardCard(Guid id, BoardCardProps props)
    {
        Id = id;
        Title = props.Title;
        SectionId = props.SectionId;
        Position = props.Position;

        if(SectionId == null != (Position == null))
        {
            SectionId = null;
            Position = null;
        }
    }

    public static BoardCard Create(Guid id, BoardCardProps props)
    {
        ArgumentNullException.ThrowIfNull(props);
        return new BoardCard(id, props);
    }

    //

    public Guid Id { get; private set; }

    public string? Title { get; internal set; }

    public Guid? SectionId { get; internal set; }

    public GridRect? Position { get; internal set; }

    public bool HasPlacement => 
        SectionId is not null && 
        Position is not null;

    public bool CanBePlaced =>
        HasPlacement &&
        !IsVerified;

    public bool IsPlaced =>
        HasPlacement &&
        IsVerified;

    public bool IsVerified { get; private set; } = false;

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

public sealed record BoardCardProps(
    string? Title,
    Guid? SectionId,
    GridRect? Position
);