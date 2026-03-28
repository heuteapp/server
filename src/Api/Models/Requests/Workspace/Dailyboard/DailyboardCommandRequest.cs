using System.Text.Json;
using HeuteApp.Core.Enums.Commands;

namespace HeuteApp.Api.Models.Requests.Workspace.Dailyboard;

public record DailyboardCommandRequest(
    string OccurredAt,
    DailyboardCommandType Type,
    JsonElement Payload
);