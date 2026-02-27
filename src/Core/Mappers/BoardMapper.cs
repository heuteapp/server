using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;

namespace HeuteApp.Core.Mappers;

public static partial class BoardMapper
{
    public static HeuteBoardProps ToProps(this HeuteBoard board)
    {
        ArgumentNullException.ThrowIfNull(board);

        return new HeuteBoardProps(
            [.. board.Cards.Select(c => c.ToSnapshot())]
        );
    }

    public static BoardCardProps ToProps(this BoardCard card)
    {
        ArgumentNullException.ThrowIfNull(card);

        return new BoardCardProps(
            card.Title,
            card.SectionId,
            card.Position
        );
    }

    public static HeuteBoardSnapshot ToSnapshot(this HeuteBoard board)
    {
        ArgumentNullException.ThrowIfNull(board);

        return new HeuteBoardSnapshot(
            board.Id,
            board.OwnerId,
            board.LayoutId,
            board.Date,
            board.ToProps()
        );
    }

    public static BoardCardSnapshot ToSnapshot(this BoardCard card)
    {
        ArgumentNullException.ThrowIfNull(card);

        return new BoardCardSnapshot(
            card.Id,
            card.ToProps()
        );
    }

    //

    public static HeuteBoard ToDomain(this HeuteBoardSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return HeuteBoard.Create(snapshot.Id, snapshot.OwnerId, snapshot.LayoutId, snapshot.Date, snapshot.Props);
    }

    public static HeuteBoard ToDomain(this HeuteBoardProps props, Guid id, Guid ownerId, Guid layoutId, DateOnly date)
    {
        ArgumentNullException.ThrowIfNull(props);

        return HeuteBoard.Create(id, ownerId, layoutId, date, props);
    }

    public static BoardCard ToDomain(this BoardCardSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return BoardCard.Create(snapshot.Id, snapshot.Props);
    }

    public static BoardCard ToDomain(this BoardCardProps props, Guid id)
    {
        ArgumentNullException.ThrowIfNull(props);

        return BoardCard.Create(id, props);
    }
}