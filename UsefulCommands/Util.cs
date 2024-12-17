using System.Collections;


public static class Util
{
    public static T? GetAtOrDefault<T>(this IReadOnlyList<T?> list, int index)
    {
        if (index < 0 || index >= list.Count) return default;
        return list[index];
    }
    
    
    public static IReadOnlyCollection<T> AsReadonly<T>(this ICollection<T> col) => new ReadonlyCollectionWrapper<T>(col);

    private class ReadonlyCollectionWrapper<T> : IReadOnlyCollection<T>
    {
        private readonly ICollection<T> _wrapAround;

        public ReadonlyCollectionWrapper(ICollection<T> wrapAround)
        {
            _wrapAround = wrapAround;
        }


        public IEnumerator<T> GetEnumerator() => _wrapAround.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _wrapAround.Count;
    }


    public static IEnumerable<string> GetFilesBroadSearch(string rootDir, string? filePattern = null)
    {
        var files = Directory.EnumerateFiles(rootDir, filePattern ?? "*", SearchOption.AllDirectories);
        var dirs = Directory.EnumerateDirectories(rootDir)
            .SelectMany(s => GetFilesBroadSearch(s, filePattern));
        return files.Concat(dirs);
    }
}