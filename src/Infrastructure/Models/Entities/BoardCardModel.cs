using HeuteApp.Core.ValueObjects;
using HeuteApp.Infrastructure.Models.Aggregates;

namespace HeuteApp.Infrastructure.Models.Entities;

public class BoardCardModel
{
    public Guid Id { get; set; }

    public Guid BoardId { get; set; }

    public HeuteBoardModel? Board { get; set; }

    public string? Title { get; set; } = string.Empty;

    public Guid? SectionId { get; set; }

    public GridRect? Position { get; set; }
}