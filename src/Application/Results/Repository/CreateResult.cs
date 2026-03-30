using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record CreateResult<T> : RepoResult
{
    public T? Entity { get; init; }

    public int? Id { get; init; }

    public RepoCreateStatus Status { get; init; }
    

    public bool IsBadRequest => Status == RepoCreateStatus.BadRequest;

    public bool IsUnauthorized => Status == RepoCreateStatus.Unauthorized;

    public bool IsForbidden => Status == RepoCreateStatus.Forbidden;

    public bool IsAlreadyExists => Status == RepoCreateStatus.AlreadyExists;

    public bool IsError => Status == RepoCreateStatus.Error;
    
    protected CreateResult() { }
    
    //
    
    public static CreateResult<T> Success(T entity, int? id = null)
    {
        return new CreateResult<T>
        {
            IsSuccess = true,
            Entity = entity,
            Id = id,
            Status = RepoCreateStatus.Success,
            StatusCode = (int)RepoCreateStatus.Success
        };
    }
    
    public static CreateResult<T> BadRequest(string errorMessage)
    {
        return new CreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoCreateStatus.BadRequest,
            StatusCode = (int)RepoCreateStatus.BadRequest
        };
    }
    
    public static CreateResult<T> Unauthorized(string? message = null)
    {
        return new CreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You are not authenticated to create this resource",
            Status = RepoCreateStatus.Unauthorized,
            StatusCode = (int)RepoCreateStatus.Unauthorized
        };
    }
    
    public static CreateResult<T> Forbidden(string? message = null)
    {
        return new CreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You do not have permission to create this resource",
            Status = RepoCreateStatus.Forbidden,
            StatusCode = (int)RepoCreateStatus.Forbidden
        };
    }
    
    public static CreateResult<T> AlreadyExists(string entityName, string identifier)
    {
        return new CreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = $"{entityName} with {identifier} already exists",
            Status = RepoCreateStatus.AlreadyExists,
            StatusCode = (int)RepoCreateStatus.AlreadyExists
        };
    }
    
    public static CreateResult<T> Error(string errorMessage)
    {
        return new CreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoCreateStatus.Error,
            StatusCode = (int)RepoCreateStatus.Error
        };
    }
    
    //
    
    public static implicit operator CreateResult<T>(T entity) => Success(entity);
}