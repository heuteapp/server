using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardCardPlacement(
    LayoutSectionKey SectionKey,
    GridRect Position
);