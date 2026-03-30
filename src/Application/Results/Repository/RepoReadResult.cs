using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record RepoReadResult<T> : RepoResult
{
    public T? Entity { get; init; }

    public RepoReadStatus Status { get; init; }

    
    public bool IsNotFound => Status == RepoReadStatus.NotFound;

    public bool IsUnauthorized => Status == RepoReadStatus.Unauthorized;

    public bool IsForbidden => Status == RepoReadStatus.Forbidden;

    public bool IsFailure => Status == RepoReadStatus.Failure;
    
    private RepoReadResult() { }

    //
    
    public static RepoReadResult<T> Success(T entity)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = true,
            Entity = entity,
            Status = RepoReadStatus.Success,
            StatusCode = (int)RepoReadStatus.Success
        };
    }
    
    public static RepoReadResult<T> Unauthorized(string? message = null)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You are not authenticated to access this resource",
            Status = RepoReadStatus.Unauthorized,
            StatusCode = (int)RepoReadStatus.Unauthorized
        };
    }
    
    public static RepoReadResult<T> Forbidden(string? message = null)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You do not have permission to access this resource",
            Status = RepoReadStatus.Forbidden,
            StatusCode = (int)RepoReadStatus.Forbidden
        };
    }
    
    public static RepoReadResult<T> NotFound(string? entityName = null)
    {
        var message = string.IsNullOrEmpty(entityName) 
            ? "The requested resource was not found"
            : $"{entityName} was not found";
            
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message,
            Status = RepoReadStatus.NotFound,
            StatusCode = (int)RepoReadStatus.NotFound
        };
    }
    
    public static RepoReadResult<T> Failure(string errorMessage)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoReadStatus.Failure,
            StatusCode = (int)RepoReadStatus.Failure
        };
    }

    //
    
    public static implicit operator RepoReadResult<T>(T entity) => Success(entity);
}