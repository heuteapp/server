using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Core.Events.Contexts;

public record BoardEventContext(
    HeuteBoard Board,
    HeuteLayout Layout
);