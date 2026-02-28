using HeuteApp.Core.Entities;
using HeuteApp.Core.Mappers;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardCardMapper
{
    public static BoardCardProps ToProps(this BoardCardModel model)
    {
        return new BoardCardProps(
            model.Title,
            model.SectionId,
            model.Position
        );
    }
}