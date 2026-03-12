using HeuteApp.Api.Models.Responses.Workspace.Board;
using HeuteApp.Application.Results.Board;

namespace HeuteApp.Api.Mappers.Workspace;

public static class BoardModelMapper
{
    public static BoardResponse ToResponse(this BoardResult board)
        => new(
            Cards: [.. board.Cards.Select(c => c.ToResponse())]
        );

    public static BoardCardResponse ToResponse(this BoardCardResult card)
        => new(
            Title: card.Content?.Title,
            SectionName: card.Placement?.SectionName,
            ColIndex: card.Placement?.ColIndex,
            RowIndex: card.Placement?.RowIndex,
            ColSpan: card.Placement?.ColSpan,
            RowSpan: card.Placement?.RowSpan
        );
}