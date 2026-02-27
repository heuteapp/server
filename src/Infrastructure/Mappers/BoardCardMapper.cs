using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Entities;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardCardMapper
{
    public static BoardCard ToDomainModel(this BoardCardEntity entity)
    {
        return BoardCard.FromProps(entity.Id, entity.ToProps());
    }

    public static BoardCardSnapshot ToSnapshot(this BoardCardEntity entity)
    {
        return new BoardCardSnapshot(
            entity.Id,
            entity.ToProps()
        );
    }

    public static BoardCardProps ToProps(this BoardCardEntity entity)
    {
        return new BoardCardProps(
            entity.Title,
            entity.SectionId,
            entity.Position
        );
    }

    public static BoardCardEntity ToEntity(this BoardCard card, Guid boardId)
    {
        return new BoardCardEntity
        {
            Id = card.Id,
            BoardId = boardId,
            Title = card.Title,
            SectionId = card.SectionId,
            Position = card.Position
        };
    }
}