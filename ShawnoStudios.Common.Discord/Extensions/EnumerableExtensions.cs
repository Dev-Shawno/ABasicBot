

namespace ShawnoStudios.Common.DiscordUtils.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> SplitInParts<T>(this IEnumerable<T> source, int chunkSize)
        {
            for (int i = 0; i < source.Count(); i += chunkSize)
            {
                yield return source.Skip(i).Take(chunkSize);
            }
        }
    }
}
