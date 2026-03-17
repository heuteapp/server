namespace HeuteApp.Application.Interfaces.Services.Category;

public sealed class CreateCategoryOptions
{
    public bool ReturnIfExists { get; init; } = false;
}