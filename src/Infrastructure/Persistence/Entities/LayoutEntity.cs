namespace HeuteApp.Infrastructure.Persistence.Entities;

public class LayoutEntity
{
    public Guid Id { get; set; }

    public Guid? OwnerId { get; set; } = null;

    public string Name { get; set; } = string.Empty;

    public int Version { get; set; } = 0;

    public List<LayoutSectionEntity> Sections { get; set; } = [];
}