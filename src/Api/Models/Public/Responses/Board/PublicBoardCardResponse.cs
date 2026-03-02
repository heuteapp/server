namespace HeuteApp.Api.Models.Public.Responses.Board;

public sealed record PublicBoardCardResponse(
    string? Title,
    string? SectionName,
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan);