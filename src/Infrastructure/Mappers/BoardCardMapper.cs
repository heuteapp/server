using HeuteApp.Core.ValueObjects.Board;
using HeuteApp.Infrastructure.Models.Board;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardCardMapper
{
    public static BoardCardProps ToProps(this BoardCardModel model)
    {
        return new BoardCardProps(
            model.Content,
            model.Placement
        );
    }
}