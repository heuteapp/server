namespace HeuteApp.Application.Results.Repository;

public abstract record RepoResult
{
    public bool IsSuccess { get; init; }

    public string? ErrorMessage { get; init; }
    
    public int StatusCode { get; init; }
}