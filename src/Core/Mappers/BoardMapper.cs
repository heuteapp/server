using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;

namespace HeuteApp.Core.Mappers;

public static partial class BoardMapper
{
    public static HeuteBoardProps ToProps(this HeuteBoard board)
    {
        ArgumentNullException.ThrowIfNull(board);

        return new HeuteBoardProps(
            [.. board.Cards.Select(c => new BoardCardSnapshot(c.Id, c.ToProps()))]
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
}