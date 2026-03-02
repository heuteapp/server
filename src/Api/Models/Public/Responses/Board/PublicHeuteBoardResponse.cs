namespace HeuteApp.Api.Models.Public.Responses.Board;

public sealed record PublicBoardCardResult(
    string? Title,
    string? SectionName,
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan);