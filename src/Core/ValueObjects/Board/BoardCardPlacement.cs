using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardPlacement(
    LayoutSectionKey Section,
    GridRect Position
);