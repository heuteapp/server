using HeuteApp.Api.Models.Responses.Category;
using HeuteApp.Application.Results.Category;

namespace HeuteApp.Api.Mappers;

public static class CategoryModelMapper
{
    public static CategoryChainResponse? ToResponseChain(this IEnumerable<CategoryResult> results)
    {
        if (!results.Any())
            return null;

        var first = results.First();
        
        return new CategoryChainResponse(
            first.Name,
            results.Skip(1).ToResponseChain()
        );
    }
}