namespace HeuteApp.Core.ValueObjects.Category;

public sealed record CategoryDefinition(
    Guid OwnerId,
    CategoryKey Key,
    CategoryProps Props
);