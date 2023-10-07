namespace WaterData.Extensions;

public static class CollectionExtensions
{
    public static ICollection<string> SelectNonEmpty(this ICollection<string> collection)
    {
        return collection
            .Where(i => !string.IsNullOrEmpty(i))
            .ToList();
    }
}
