namespace HeuteApp.Api.Models.Responses.Workspace.Board;

public record BoardCardResponse(
    string? Title,
    string? SectionName,
    int? ColIndex,
    int? RowIndex,
    int? ColSpan,
    int? RowSpan
);