namespace HeuteApp.Core.ValueObjects.Profile;

public sealed record ProfileDefinition(
    Guid Id,
    string Username,
    string Email)
{
    public static ProfileDefinition Empty => new(Guid.Empty, string.Empty, string.Empty);
}