using HeuteApp.Core.Aggregates.User;
using HeuteApp.Core.ValueObjects.User;

namespace HeuteApp.Infrastructure.Models.User;

public class HeuteUserModel : HeuteUser
{
    protected HeuteUserModel() { }

    protected HeuteUserModel(UserDefinition definition) : base(definition) { }

    //

    public static new HeuteUserModel Create(UserDefinition definition)
    {
        return new HeuteUserModel(definition);
    }
}