using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Mappers;

public static partial class BoardMapper
{
    public static BoardCardProps ToProps(this BoardCard card)
    {
        ArgumentNullException.ThrowIfNull(card);

        return new BoardCardProps(
            card.Title,
            card.SectionId,
            card.Position
        );
    }

    public static BoardCard ToDomain(this BoardCardProps props, Guid id)
    {
        ArgumentNullException.ThrowIfNull(props);

        return BoardCard.Create(id, props);
    }
}