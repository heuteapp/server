namespace HeuteApp.Api.Models.Responses.Category;

public record CategoryChainResponse(
    string Name,
    CategoryChainResponse? Child
);