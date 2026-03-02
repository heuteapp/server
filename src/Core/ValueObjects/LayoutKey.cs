using HeuteApp.Core.Enums;

namespace HeuteApp.Core.ValueObjects;

public sealed record LayoutKey(
    LayoutScopeType Scope,
    string Name,
    int Version
);