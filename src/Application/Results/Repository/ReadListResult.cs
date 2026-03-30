using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record ReadListResult<T> : RepoResult
{
    public IEnumerable<T>? Entities { get; init; }

    public int? TotalCount { get; init; }

    public RepoReadStatus Status { get; init; }
    
    
    public bool IsEmpty => Status == RepoReadStatus.Success && (Entities == null || !Entities.Any());

    public bool IsNotFound => Status == RepoReadStatus.NotFound;

    public bool IsUnauthorized => Status == RepoReadStatus.Unauthorized;

    public bool IsForbidden => Status == RepoReadStatus.Forbidden;
    
    public bool IsError => Status == RepoReadStatus.Error;
    
    
    private ReadListResult() { }
    
    //
    
    public static ReadListResult<T> Success(IEnumerable<T> entities, int? totalCount = null)
    {
        var entityList = entities?.ToList() ?? new List<T>();
        return new ReadListResult<T>
        {
            IsSuccess = true,
            Entities = entityList,
            TotalCount = totalCount ?? entityList.Count,
            Status = RepoReadStatus.Success,
            StatusCode = (int)RepoReadStatus.Success
        };
    }
    
    public static ReadListResult<T> Unauthorized(string? message = null)
    {
        return new ReadListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You are not authenticated to access this resource",
            Status = RepoReadStatus.Unauthorized,
            StatusCode = (int)RepoReadStatus.Unauthorized
        };
    }
    
    public static ReadListResult<T> Forbidden(string? message = null)
    {
        return new ReadListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You do not have permission to access this resource",
            Status = RepoReadStatus.Forbidden,
            StatusCode = (int)RepoReadStatus.Forbidden
        };
    }
    
    public static ReadListResult<T> NotFound(string? entityName = null)
    {
        var message = string.IsNullOrEmpty(entityName) 
            ? "The requested resource was not found"
            : $"{entityName} was not found";
            
        return new ReadListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message,
            Status = RepoReadStatus.NotFound,
            StatusCode = (int)RepoReadStatus.NotFound
        };
    }
    
    public static ReadListResult<T> Error(string errorMessage)
    {
        return new ReadListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoReadStatus.Error,
            StatusCode = (int)RepoReadStatus.Error
        };
    }
    
    //
    
    public static implicit operator ReadListResult<T>(List<T> entities) => Success(entities);

    public static implicit operator ReadListResult<T>(T[] entities) => Success(entities);
}