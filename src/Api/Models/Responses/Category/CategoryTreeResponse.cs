namespace HeuteApp.Api.Models.Responses.Category;

public record CategoryTreeResponse(
    string Name,
    IEnumerable<CategoryTreeResponse>? Children
);