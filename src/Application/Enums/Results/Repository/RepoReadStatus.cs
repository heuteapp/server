namespace HeuteApp.Application.Enums.Results.Repository;

public enum RepoReadStatus
{
    Success = 200,

    Unauthorized = 401,

    Forbidden = 403,

    NotFound = 404,

    Failure = 500
}