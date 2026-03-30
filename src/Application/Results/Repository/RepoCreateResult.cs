using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record RepoCreateResult<T> : RepoResult
{
    public T? Entity { get; init; }

    public int? Id { get; init; }

    public RepoCreateStatus Status { get; init; }
    

    public bool IsBadRequest => Status == RepoCreateStatus.BadRequest;

    public bool IsUnauthorized => Status == RepoCreateStatus.Unauthorized;

    public bool IsForbidden => Status == RepoCreateStatus.Forbidden;

    public bool IsAlreadyExists => Status == RepoCreateStatus.AlreadyExists;

    public bool IsFailure => Status == RepoCreateStatus.Failure;
    
    protected RepoCreateResult() { }
    
    //
    
    public static RepoCreateResult<T> Success(T entity, int? id = null)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = true,
            Entity = entity,
            Id = id,
            Status = RepoCreateStatus.Success,
            StatusCode = (int)RepoCreateStatus.Success
        };
    }
    
    public static RepoCreateResult<T> BadRequest(string errorMessage)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoCreateStatus.BadRequest,
            StatusCode = (int)RepoCreateStatus.BadRequest
        };
    }
    
    public static RepoCreateResult<T> Unauthorized(string? message = null)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You are not authenticated to create this resource",
            Status = RepoCreateStatus.Unauthorized,
            StatusCode = (int)RepoCreateStatus.Unauthorized
        };
    }
    
    public static RepoCreateResult<T> Forbidden(string? message = null)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You do not have permission to create this resource",
            Status = RepoCreateStatus.Forbidden,
            StatusCode = (int)RepoCreateStatus.Forbidden
        };
    }
    
    public static RepoCreateResult<T> AlreadyExists(string entityName, string identifier)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = $"{entityName} with {identifier} already exists",
            Status = RepoCreateStatus.AlreadyExists,
            StatusCode = (int)RepoCreateStatus.AlreadyExists
        };
    }
    
    public static RepoCreateResult<T> Failure(string errorMessage)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoCreateStatus.Failure,
            StatusCode = (int)RepoCreateStatus.Failure
        };
    }
    
    //
    
    public static implicit operator RepoCreateResult<T>(T entity) => Success(entity);
}