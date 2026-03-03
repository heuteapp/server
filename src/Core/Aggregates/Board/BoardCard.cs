using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Aggregates.Board;

public class BoardCard
{
    protected BoardCard() { }

    protected BoardCard(Guid id, BoardCardProps props)
    {
        Id = id;
        Title = props.Title;
        Placement = props.Placement;

        if(Placement == null)
        {
            return;
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

    public BoardCardPlacement? Placement { get; private set; }

    public bool HasPlacement => Placement is not null;

    public bool CanBePlaced => HasPlacement && !IsVerified;

    public bool IsPlaced => HasPlacement && IsVerified;

    public bool IsVerified { get; private set; } = false;

    //

    internal void DoPlace(BoardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(placement);

        Placement = placement;
        IsVerified = false;
    }

    internal void DoUnplace()
    {
        Placement = null;
        IsVerified = false;
    }

    internal void DoVerify()
    {
        IsVerified = true;
    }
}