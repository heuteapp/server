namespace HeuteApp.Api.Models.Requests.Workspace.Dailyboard;

public record DailyboardCommandsRequest(
    IEnumerable<DailyboardCommandRequest> Commands
);