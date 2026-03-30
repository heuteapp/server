using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record RepoReadResult<T> : RepoResult
{
    public T? Entity { get; init; }

    public RepoResultStatus Status { get; init; }
    
    public static RepoReadResult<T> Success(T entity)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = true,
            Entity = entity,
            Status = RepoResultStatus.Success
        };
    }
    
    public static RepoReadResult<T> NotFound(string entityName)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = $"{entityName} not found",
            Status = RepoResultStatus.NotFound
        };
    }
    
    public static RepoReadResult<T> Unauthorized()
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = "Unauthorized access",
            Status = RepoResultStatus.Unauthorized
        };
    }
}