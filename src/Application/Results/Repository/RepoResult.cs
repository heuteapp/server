namespace HeuteApp.Application.Results.Repository;

public abstract record RepoResult
{
    public bool IsSuccess { get; init; }

    public string? ErrorMessage { get; init; }
    
    public int StatusCode { get; init; }


    protected static T Failure<T>(string errorMessage) where T : RepoResult, new()
    {
        return new T
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            StatusCode = 500
        };
    }
}