namespace HeuteApp.Core.ValueObjects.Board;

public record BoardCardContent
{
    public string Title { get; init; } = null!;

    //

    private BoardCardContent() { }

    public BoardCardContent(string title)
    {
        Title = title;
    }
}