using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record RepoReadResult<T> : RepoResult
{
    public T? Entity { get; init; }

    public RepoReadStatus Status { get; init; }

    
    public static RepoReadResult<T> Success(T entity)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = true,
            Entity = entity,
            Status = RepoReadStatus.Success,
            StatusCode = 200
        };
    }
    
    public static RepoReadResult<T> Unauthorized()
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = "Unauthorized access",
            Status = RepoReadStatus.Unauthorized,
            StatusCode = 401
        };
    }

    public static RepoReadResult<T> Forbidden()
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = "Forbidden access",
            Status = RepoReadStatus.Forbidden,
            StatusCode = 403
        };
    }

    public static RepoReadResult<T> NotFound(string entityName)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = $"{entityName} not found",
            Status = RepoReadStatus.NotFound,
            StatusCode = 404
        };
    }

    public static RepoReadResult<T> Failure(string errorMessage)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoReadStatus.Failure,
            StatusCode = 500
        };
    }
}