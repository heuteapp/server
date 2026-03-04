using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Core.ValueObjects.Category;
using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Api.Models.Public.Request;

public record CreateBoardRequest(
    CategoryKey Category,
    LayoutKey Layout,
    BoardDefinition Definition
);