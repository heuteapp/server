namespace HeuteApp.Api.Models.Responses.Dailyboard;

public record DailyboardCardResponse(
    string Name,
    string? Title,
    string? SectionName,
    int? ColIndex,
    int? RowIndex,
    int? ColSpan,
    int? RowSpan
);