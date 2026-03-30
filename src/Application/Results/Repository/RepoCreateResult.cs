using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record RepoCreateResult<T> : RepoResult
{
    public T? Entity { get; init; }

    public int? Id { get; init; }

    public RepoCreateStatus Status { get; init; }

    
    public static RepoCreateResult<T> Success(T entity, int? id = null)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = true,
            Entity = entity,
            Id = id,
            Status = RepoCreateStatus.Success,
            StatusCode = 201
        };
    }
    
    public static RepoCreateResult<T> BadRequest(string errorMessage)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoCreateStatus.BadRequest,
            StatusCode = 400
        };
    }
    
    public static RepoCreateResult<T> Unauthorized(string? message = null)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "Unauthorized access",
            Status = RepoCreateStatus.Unauthorized,
            StatusCode = 401
        };
    }
    
    public static RepoCreateResult<T> Forbidden(string? message = null)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "Forbidden access",
            Status = RepoCreateStatus.Forbidden,
            StatusCode = 403
        };
    }
    
    public static RepoCreateResult<T> AlreadyExists(string entityName, string identifier)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = $"{entityName} with {identifier} already exists",
            Status = RepoCreateStatus.AlreadyExists,
            StatusCode = 409
        };
    }
    
    public static RepoCreateResult<T> Failure(string errorMessage)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Status = RepoCreateStatus.Failure,
            StatusCode = 500
        };
    }
}