using HeuteApp.Application.Enums.Results.Repository;

namespace HeuteApp.Application.Results.Repository;

public record CreateListResult<T> : RepoResult
{
    public IEnumerable<T>? Entities { get; init; }

    public IEnumerable<int>? Ids { get; init; }

    public RepoCreateStatus Status { get; init; }
    
    public bool IsBadRequest => Status == RepoCreateStatus.BadRequest;

    public bool IsUnauthorized => Status == RepoCreateStatus.Unauthorized;

    public bool IsForbidden => Status == RepoCreateStatus.Forbidden;

    public bool IsAlreadyExists => Status == RepoCreateStatus.AlreadyExists;

    public bool IsError => Status == RepoCreateStatus.Error;
    
    protected CreateListResult() { }
    
    //
    
    public static CreateListResult<T> Success(IEnumerable<T> entities, IEnumerable<int>? ids = null)
    {
        return new CreateListResult<T>
        {
            IsSuccess = true,
            Entities = entities,
            Ids = ids,
            Status = RepoCreateStatus.Success,
            StatusCode = (int)RepoCreateStatus.Success
        };
    }
    
    public static CreateListResult<T> BadRequest(string errorMessage, IEnumerable<T>? entities = null)
    {
        return new CreateListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Entities = entities,
            Status = RepoCreateStatus.BadRequest,
            StatusCode = (int)RepoCreateStatus.BadRequest
        };
    }
    
    public static CreateListResult<T> Unauthorized(string? message = null, IEnumerable<T>? entities = null)
    {
        return new CreateListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You are not authenticated to create these resources",
            Entities = entities,
            Status = RepoCreateStatus.Unauthorized,
            StatusCode = (int)RepoCreateStatus.Unauthorized
        };
    }
    
    public static CreateListResult<T> Forbidden(string? message = null, IEnumerable<T>? entities = null)
    {
        return new CreateListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = message ?? "You do not have permission to create these resources",
            Entities = entities,
            Status = RepoCreateStatus.Forbidden,
            StatusCode = (int)RepoCreateStatus.Forbidden
        };
    }
    
    public static CreateListResult<T> AlreadyExists(string entityName, string identifier, IEnumerable<T>? entities = null)
    {
        return new CreateListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = $"{entityName} with {identifier} already exists",
            Entities = entities,
            Status = RepoCreateStatus.AlreadyExists,
            StatusCode = (int)RepoCreateStatus.AlreadyExists
        };
    }
    
    public static CreateListResult<T> Error(string errorMessage, IEnumerable<T>? entities = null)
    {
        return new CreateListResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Entities = entities,
            Status = RepoCreateStatus.Error,
            StatusCode = (int)RepoCreateStatus.Error
        };
    }
    
    //
    
    public static implicit operator CreateListResult<T>(List<T> entities) => Success(entities);
}