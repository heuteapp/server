namespace HeuteApp.Application.Results.Category;

public record CategoryHierarchyResult(
    IEnumerable<CategoryTreeResult> Roots);