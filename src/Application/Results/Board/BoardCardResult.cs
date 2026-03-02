namespace HeuteApp.Application.Results.Board;

public sealed record BoardCardResult(
    Guid Id,
    string? Title,
    Guid? SectionId,
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan);