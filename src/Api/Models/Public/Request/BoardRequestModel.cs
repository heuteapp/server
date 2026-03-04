using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Api.Models.Public.Request;

public record CreateBoardRequest(LayoutKey Layout, BoardKey Key, BoardProps Props);