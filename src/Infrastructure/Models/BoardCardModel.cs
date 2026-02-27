using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Infrastructure.Models;

public class BoardCardModel
{
    public Guid Id { get; set; }

    public Guid BoardId { get; set; }

    public BoardModel? Board { get; set; } = null;
    
    public string Title { get; set; } = string.Empty;

    public Guid? SectionId { get; set; }

    public GridRect? Position { get; set; }
}