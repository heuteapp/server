using HeuteApp.Core.Aggregates.Category;
using HeuteApp.Application.Enums.Results.Category.Repository;

namespace HeuteApp.Application.Results.Category.Repository;

public record CategoryCreateResult
{
    public HeuteCategory? Category { get; init; }

    public CategoryCreateStatus Status { get; init; }
    
    public string? ExistingName { get; init; }
    
    public bool IsSuccess => Status == CategoryCreateStatus.Success;
}