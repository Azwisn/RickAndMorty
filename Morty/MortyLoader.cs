static class MortyLoader
{
    public static IMorty? Load(string name)
    {
        if (string.Equals(name, "ClassicMorty", StringComparison.OrdinalIgnoreCase)) return new ClassicMorty();
        if (string.Equals(name, "LazyMorty", StringComparison.OrdinalIgnoreCase)) return new LazyMorty();
        return null;
    }
}
