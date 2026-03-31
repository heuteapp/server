namespace HeuteApp.Api.Models.Responses.Category;

public record CategoryTreeResponse(
    string Name,
    IEnumerable<CategoryTreeResponse>? Children
);

public record CategoryRootTreeResponse(string Name = "/", IEnumerable<CategoryTreeResponse>? Children = null) 
    : CategoryTreeResponse(Name, Children);