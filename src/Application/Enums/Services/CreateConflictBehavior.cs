namespace HeuteApp.Application.Enums.Services;

public enum CreateConflictBehavior
{
    /// <summary>
    /// Throws an exception if a conflict is detected
    /// </summary>
    Strict,

    /// <summary>
    /// Returns the existing entity if a conflict is detected, otherwise creates a new one
    /// </summary>
    ReturnExisting,
}