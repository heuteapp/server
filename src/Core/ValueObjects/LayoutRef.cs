using HeuteApp.Core.Enums;

namespace HeuteApp.Core.ValueObjects;

public sealed record LayoutRef(
    LayoutScopeType Scope,
    string Name,
    int Version
);