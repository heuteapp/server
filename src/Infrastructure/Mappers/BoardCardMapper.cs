using HeuteApp.Core.Entities;
using HeuteApp.Core.Mappers;
using HeuteApp.Infrastructure.Models;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardCardMapper
{
    public static BoardCard ToDomain(this BoardCardModel entity)
    {
        return entity.ToProps().ToDomain(entity.Id);
    }

    public static BoardCardProps ToProps(this BoardCardModel entity)
    {
        return new BoardCardProps(
            entity.Title,
            entity.SectionId,
            entity.Position
        );
    }

    public static BoardCardModel ToModel(this BoardCard card, Guid boardId)
    {
        return new BoardCardModel
        {
            Id = card.Id,
            BoardId = boardId,
            Title = card.Title ?? null!,
            SectionId = card.SectionId,
            Position = card.Position
        };
    }
}