using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Infrastructure.Persistence.Entities;

public class BoardCardEntity
{
    public Guid Id { get; set; }

    public Guid BoardId { get; set; }

    public BoardEntity? Board { get; set; } = null;
    
    public string Title { get; set; } = string.Empty;

    public Guid? SectionId { get; set; }

    public GridRect? Position { get; set; }
}