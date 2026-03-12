using HeuteApp.Api.Models.Responses.Workspace.Board;
using HeuteApp.Core.Aggregates.Board;

namespace HeuteApp.Api.Mappers.Workspace;

public static class BoardModelMapper
{
    public static BoardResponse ToResponse(this HeuteBoard board)
    {
        var cards = board.Cards.Select(card => new BoardCardResponse(
            Title: card.Content.Title,
            SectionName: card.Placement?.SectionName,
            ColIndex: card.Placement?.ColIndex,
            RowIndex: card.Placement?.RowIndex,
            ColSpan: card.Placement?.ColSpan,
            RowSpan: card.Placement?.RowSpan
        )).ToList();

        return new BoardResponse(cards);
    }
}