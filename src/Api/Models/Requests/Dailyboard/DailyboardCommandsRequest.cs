namespace HeuteApp.Api.Models.Requests.Dailyboard;

public record DailyboardCommandsRequest(
    IEnumerable<DailyboardCommandRequest> Commands
);