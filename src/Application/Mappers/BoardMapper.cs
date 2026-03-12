using HeuteApp.Application.Results.Board;
using HeuteApp.Core.Aggregates.Board;

namespace HeuteApp.Application.Mappers;

public static class BoardMapper
{
    public static BoardResult ToResult(this HeuteBoard board)
    {
        ArgumentNullException.ThrowIfNull(board);

        return new BoardResult(
            board.Id,
            board.OwnerId,
            board.LayoutId,
            board.CategoryId,
            board.Date,
            [..board.Cards.Select(ToResult)]
        );
    }

    public static BoardCardResult ToResult(this BoardCard card)
    {
        ArgumentNullException.ThrowIfNull(card);

        return new BoardCardResult(
            card.Id,
            card.Name,
            card.Content,
            card.Placement
        );
    }
}