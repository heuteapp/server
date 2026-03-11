using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Mappers;

public static partial class BoardMapper
{
    public static BoardCardProps ToProps(this BoardCard card)
    {
        ArgumentNullException.ThrowIfNull(card);

        return new BoardCardProps(
            card.Content,
            card.Placement
        );
    }

    public static BoardCard ToDomain(this BoardCardDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        return BoardCard.Create(definition);
    }
}