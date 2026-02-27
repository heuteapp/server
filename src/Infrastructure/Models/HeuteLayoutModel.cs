namespace HeuteApp.Infrastructure.Models;

public class HeuteLayoutModel
{
    public Guid Id { get; set; }

    public Guid? OwnerId { get; set; } = null;

    public string Name { get; set; } = string.Empty;

    public int Version { get; set; } = 0;

    public List<LayoutSectionModel> Sections { get; set; } = [];
}