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
}