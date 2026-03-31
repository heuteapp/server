namespace HeuteApp.Application.Results.Category;

public record CategoryChainResult(
    Guid Id,
    Guid UserId,
    string Name,
    CategoryChainResult? Child);