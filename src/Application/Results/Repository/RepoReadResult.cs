namespace HeuteApp.Application.Results.Repository;

public record RepoReadResult<T> : RepoResult
{
    public T? Entity { get; init; }

    
    public static RepoReadResult<T> Success(T entity)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = true,
            Entity = entity
        };
    }
    
    public static RepoReadResult<T> Failure(string errorMessage)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
    
    public static RepoReadResult<T> NotFound(string entityName)
    {
        return new RepoReadResult<T>
        {
            IsSuccess = false,
            ErrorMessage = $"{entityName} not found"
        };
    }
}