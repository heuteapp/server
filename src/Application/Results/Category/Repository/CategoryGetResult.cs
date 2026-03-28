using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Application.Enums.Results.Category.Repository;

namespace HeuteApp.Application.Results.Category.Repository;

public record CategoryGetResult
{
    public HeuteCategory? Category { get; init; }
    
    public CategoryGetStatus Status { get; init; }
    
    public bool IsSuccess => Status == CategoryGetStatus.Success;
}