using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Mappers;

public static partial class ChainMapper
{
    public static T ToLast<T> (this Chain<T> chain)
    {
        ArgumentNullException.ThrowIfNull(chain);
        Chain<T>? current = chain;

        while (current.Child != null)
            current = current.Child;

        return current.Current;
    }
}