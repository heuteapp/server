using HeuteApp.Api.Models.Responses.Workspace.Board;
using HeuteApp.Core.Aggregates.Board;

namespace HeuteApp.Api.Mappers.Workspace;

public static class BoardModelMapper
{
    public static BoardResponse ToResponse(this HeuteBoard board)
        => new(
            Cards: [.. board.Cards.Select(c => c.ToResponse())]
        );

    public static BoardCardResponse ToResponse(this BoardCard card)
        => new(
            Title: card.Content.Title,
            SectionName: card.Placement?.SectionName,
            ColIndex: card.Placement?.ColIndex,
            RowIndex: card.Placement?.RowIndex,
            ColSpan: card.Placement?.ColSpan,
            RowSpan: card.Placement?.RowSpan
        );
}