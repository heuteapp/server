using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record ReadResult<T> : RepoResult
{
    public T? Entity { get; init; }

    public RepoReadStatus Status { get; init; }

    
    public bool IsNotFound => Status == RepoReadStatus.NotFound;

    public bool IsUnauthorized => Status == RepoReadStatus.Unauthorized;

    public bool IsForbidden => Status == RepoReadStatus.Forbidden;

    public bool IsError => Status == RepoReadStatus.Error;
    
    protected ReadResult() { }

    //
    
    public static ReadResult<T> Success(T entity)
    {
        return new ReadResult<T>
        {
            IsSuccess = true,
            Entity = entity,
            Status = RepoReadStatus.Success,
            StatusCode = (int)RepoReadStatus.Success
        };
    }
    
    public static ReadResult<T> Unauthorized(string? message = null)
    {
        return new ReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You are not authenticated to access this resource",
            Status = RepoReadStatus.Unauthorized,
            StatusCode = (int)RepoReadStatus.Unauthorized
        };
    }
    
    public static ReadResult<T> Forbidden(string? message = null)
    {
        return new ReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You do not have permission to access this resource",
            Status = RepoReadStatus.Forbidden,
            StatusCode = (int)RepoReadStatus.Forbidden
        };
    }
    
    public static ReadResult<T> NotFound(string? entityName = null)
    {
        var message = string.IsNullOrEmpty(entityName) 
            ? "The requested resource was not found"
            : $"{entityName} was not found";
            
        return new ReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message,
            Status = RepoReadStatus.NotFound,
            StatusCode = (int)RepoReadStatus.NotFound
        };
    }
    
    public static ReadResult<T> Error(string errorMessage)
    {
        return new ReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoReadStatus.Error,
            StatusCode = (int)RepoReadStatus.Error
        };
    }

    //
    
    public static implicit operator ReadResult<T>(T entity) => Success(entity);
}