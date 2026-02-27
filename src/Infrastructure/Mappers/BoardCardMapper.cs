using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Models;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardCardMapper
{
    public static BoardCard ToDomainModel(this BoardCardModel entity)
    {
        return BoardCard.FromProps(entity.Id, entity.ToProps());
    }

    public static BoardCardSnapshot ToSnapshot(this BoardCardModel entity)
    {
        return new BoardCardSnapshot(
            entity.Id,
            entity.ToProps()
        );
    }

    public static BoardCardProps ToProps(this BoardCardModel entity)
    {
        return new BoardCardProps(
            entity.Title,
            entity.SectionId,
            entity.Position
        );
    }

    public static BoardCardModel ToEntity(this BoardCard card, Guid boardId)
    {
        return new BoardCardModel
        {
            Id = card.Id,
            BoardId = boardId,
            Title = card.Title,
            SectionId = card.SectionId,
            Position = card.Position
        };
    }
}