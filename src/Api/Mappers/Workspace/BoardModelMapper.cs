using HeuteApp.Api.Models.Responses.Workspace.Board;
using HeuteApp.Application.Results.Board;
using HeuteApp.Application.Services;

namespace HeuteApp.Api.Mappers.Workspace;

public static class BoardModelMapper
{
    public static async Task<BoardResponse> ToResponse(this BoardResult board, LayoutService layoutService)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(layoutService);

        var layout = await layoutService.GetLayoutByIdAsync(board.LayoutId)
            ?? throw new Exception("Layout not found.");

        return new BoardResponse(
            LayoutName: layout.Name,
            LayoutVersion: layout.Version,
            Cards: [.. board.Cards.Select(ToResponse)]
        );
    }

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