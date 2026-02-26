using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Persistence.Entities;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardCardMapper
{
    public static HeuteBoardCard ToDomainModel(this BoardCardEntity entity)
    {
        return HeuteBoardCard.FromSnapshot(entity.ToSnapshot());
    }

    public static HeuteBoardCardSnapshot ToSnapshot(this BoardCardEntity entity)
    {
        return new HeuteBoardCardSnapshot(
            entity.Id,
            new HeuteBoardCardProps(
                entity.Title,
                entity.SectionId,
                entity.Position
            )
        );
    }
}