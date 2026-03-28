namespace HeuteApp.Application.Enums.Results.Profile.Repository;

public enum ProfileCreateStatus
{
    Success,

    UsernameAlreadyExists,

    EmailAlreadyExists,
    
    InvalidData
}