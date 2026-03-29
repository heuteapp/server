namespace HeuteApp.Application.Results.Category;

public record CategoryResult(
    Guid Id,
    Guid UserId,
    Guid? ParentId,
    string Name);