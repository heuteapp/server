using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;

namespace HeuteApp.Core.Mappers;

public static partial class BoardMapper
{
    public static HeuteBoardProps ToProps(this HeuteBoard board)
    {
        ArgumentNullException.ThrowIfNull(board);

        return new HeuteBoardProps(
            [.. board.Cards]
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

    //

    public static HeuteBoard ToDomain(this HeuteBoardProps props, Guid id, Guid ownerId, Guid layoutId, DateOnly date)
    {
        ArgumentNullException.ThrowIfNull(props);

        return HeuteBoard.Create(id, ownerId, layoutId, date, props);
    }

    public static BoardCard ToDomain(this BoardCardProps props, Guid id)
    {
        ArgumentNullException.ThrowIfNull(props);

        return BoardCard.Create(id, props);
    }
}