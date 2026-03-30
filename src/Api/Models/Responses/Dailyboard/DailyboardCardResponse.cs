namespace HeuteApp.Api.Models.Responses.Workspace.Dailyboard;

public record DailyboardCardResponse(
    string Name,
    string? Title,
    string? SectionName,
    int? ColIndex,
    int? RowIndex,
    int? ColSpan,
    int? RowSpan
);