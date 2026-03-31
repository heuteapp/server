using HeuteApp.Api.Models.Responses.Category;
using HeuteApp.Application.Results.Category;

namespace HeuteApp.Api.Mappers;

public static class CategoryModelMapper
{
    public static CategoryChainResponse? ToResponse(this CategoryChainResult result)
    {
        if (result == null)
            return null;

        return new CategoryChainResponse(
            result.Name,
            result.Child?.ToResponse()
        );
    }
}