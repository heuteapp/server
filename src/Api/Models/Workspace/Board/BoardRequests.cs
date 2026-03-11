using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Api.Models.Workspace.Board;

public record SyncBoardRequest(
    BoardProps Props
);