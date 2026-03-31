namespace HeuteApp.Application.Results.Category;

public record CategoryTreeResult(
    Guid Id,
    Guid UserId,
    string Name,
    IEnumerable<CategoryTreeResult>? Children);