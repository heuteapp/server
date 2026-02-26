using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Persistence.Entities;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardCardMapper
{
    public static HeuteBoardCard ToDomainModel(this BoardCardEntity entity)
    {
        return HeuteBoardCard.FromProps(entity.Id, entity.ToProps());
    }

    public static HeuteBoardCardSnapshot ToSnapshot(this BoardCardEntity entity)
    {
        return new HeuteBoardCardSnapshot(
            entity.Id,
            entity.ToProps()
        );
    }

    public static HeuteBoardCardProps ToProps(this BoardCardEntity entity)
    {
        return new HeuteBoardCardProps(
            entity.Title,
            entity.SectionId,
            entity.Position
        );
    }

    public static BoardCardEntity ToEntity(this HeuteBoardCard card, Guid boardId)
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