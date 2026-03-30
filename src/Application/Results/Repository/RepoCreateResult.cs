namespace HeuteApp.Application.Results.Repository;

public record RepoCreateResult<T> : RepoResult
{
    public T? Entity { get; init; }

    public int? Id { get; init; }

    
    public static RepoCreateResult<T> Success(T entity, int? id = null)
    {
        return new RepoCreateResult<T>
        {
            IsSuccess = true,
            Entity = entity,
            Id = id
        };
    }
    
    public static RepoCreateResult<T> Failure(string errorMessage)
    {
        return Failure<RepoCreateResult<T>>(errorMessage);
    }
}