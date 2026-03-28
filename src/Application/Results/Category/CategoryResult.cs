namespace HeuteApp.Application.Results.Category;

public record CategoryResult(
    Guid Id,
    Guid OwnerId,
    Guid? ParentId,
    string Name);