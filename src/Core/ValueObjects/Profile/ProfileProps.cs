namespace HeuteApp.Core.ValueObjects.Profile;

public sealed record ProfileProps(string Username, string Email)
{
    public static ProfileProps Empty => new(string.Empty, string.Empty);
}