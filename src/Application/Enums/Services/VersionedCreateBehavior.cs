namespace HeuteApp.Application.Enums.Services;

public enum VersionedCreateBehavior
{
    /// <summary>
    /// Create always a new versioned entity, even if an existing one already exists.
    /// </summary>
    CreateNew,

    /// <summary>
    /// If an existing versioned entity exists, return the latest version instead of creating a new one.
    /// </summary>
    ReturnLatest
}