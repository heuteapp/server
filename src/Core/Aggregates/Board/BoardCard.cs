using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Aggregates.Board;

public class BoardCard
{
    protected BoardCard() { }

    protected BoardCard(BoardCardDefinition definition)
    {
        Id = Guid.NewGuid();
        Title = definition.Props.Title;
        Placement = definition.Props.Placement;

        if(Placement == null)
        {
            return;
        }
    }

    public static BoardCard Create(BoardCardDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        return new BoardCard(definition);
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