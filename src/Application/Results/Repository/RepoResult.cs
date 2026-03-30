namespace HeuteApp.Application.Results.Repository;

public abstract record RepoResult
{
    public bool IsSuccess { get; init; }

    public string? ErrorMessage { get; init; }
    
    public int StatusCode { get; init; }
    

    public void ThrowIfFailure(string message = "An error occurred while processing the repository operation")
    {
        if (!IsSuccess)
        {
            throw new Exception($"{message}: \nMessage: {ErrorMessage} \nStatus: {StatusCode}");
        }
    }
}