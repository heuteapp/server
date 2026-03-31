namespace HeuteApp.Api.Models.Responses.Category;

public record CategoryHierarchyResponse(
    List<CategoryTreeResponse> Roots
);