using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Core.Commands.Contexts;

public record BoardCommandContext(
    HeuteBoard Board,
    HeuteLayout Layout
);