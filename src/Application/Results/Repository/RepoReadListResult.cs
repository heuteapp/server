using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record RepoReadListResult<T> : RepoResult
{
    public IEnumerable<T>? Entities { get; init; }

    public int? TotalCount { get; init; }

    public RepoReadStatus Status { get; init; }
    
    
    public bool IsEmpty => Status == RepoReadStatus.Success && (Entities == null || !Entities.Any());

    public bool IsNotFound => Status == RepoReadStatus.NotFound;

    public bool IsUnauthorized => Status == RepoReadStatus.Unauthorized;

    public bool IsForbidden => Status == RepoReadStatus.Forbidden;
    
    public bool IsFailure => Status == RepoReadStatus.Failure;
    
    
    private RepoReadListResult() { }
    
    //
    
    public static RepoReadListResult<T> Success(IEnumerable<T> entities, int? totalCount = null)
    {
        var entityList = entities?.ToList() ?? new List<T>();
        return new RepoReadListResult<T>
        {
            IsSuccess = true,
            Entities = entityList,
            TotalCount = totalCount ?? entityList.Count,
            Status = RepoReadStatus.Success,
            StatusCode = (int)RepoReadStatus.Success
        };
    }
    
    public static RepoReadListResult<T> Unauthorized(string? message = null)
    {
        return new RepoReadListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You are not authenticated to access this resource",
            Status = RepoReadStatus.Unauthorized,
            StatusCode = (int)RepoReadStatus.Unauthorized
        };
    }
    
    public static RepoReadListResult<T> Forbidden(string? message = null)
    {
        return new RepoReadListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You do not have permission to access this resource",
            Status = RepoReadStatus.Forbidden,
            StatusCode = (int)RepoReadStatus.Forbidden
        };
    }
    
    public static RepoReadListResult<T> NotFound(string? entityName = null)
    {
        var message = string.IsNullOrEmpty(entityName) 
            ? "The requested resource was not found"
            : $"{entityName} was not found";
            
        return new RepoReadListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message,
            Status = RepoReadStatus.NotFound,
            StatusCode = (int)RepoReadStatus.NotFound
        };
    }
    
    public static RepoReadListResult<T> Failure(string errorMessage)
    {
        return new RepoReadListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoReadStatus.Failure,
            StatusCode = (int)RepoReadStatus.Failure
        };
    }
    
    //
    
    public static implicit operator RepoReadListResult<T>(List<T> entities) => Success(entities);

    public static implicit operator RepoReadListResult<T>(T[] entities) => Success(entities);
}