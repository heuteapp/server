using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Application.Results.Layout;

public record LayoutSectionResult(
    Guid Id,
    string Name,
    GridRect Area);