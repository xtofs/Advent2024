namespace console.Extensions;

public static class EnumerableExtensions
{
    public static IReadOnlyList<IReadOnlyList<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> enumerable)
    {
        var result = new List<List<T>>();
        foreach (var (row, r) in enumerable.Select((row, r) => (row, r)))
        {
            foreach (var (item, c) in row.Select((item, c) => (item, c)))
            {
                if (result.Count <= c) { result.Add([]); }
                result[c].Add(item);
            }
        }
        return result;
    }


    public static IEnumerable<IReadOnlyList<T>> Window<T>(this IEnumerable<T> enumerable, int size)
    {
        var window = new List<T>();
        foreach (var (item, i) in enumerable.Select((item, i) => (item, i)))
        {
            window.Add(item);
            if (window.Count == size)
            {
                yield return window;
                window.RemoveAt(0);
            }
        }
    }
}