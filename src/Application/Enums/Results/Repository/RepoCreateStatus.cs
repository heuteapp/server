namespace HeuteApp.Application.Enums.Results.Repository;

public enum RepoCreateStatus
{
    Success = 201,

    BadRequest = 400,

    Unauthorized = 401,

    Forbidden = 403,
    
    AlreadyExists = 409,

    Failure = 500
}