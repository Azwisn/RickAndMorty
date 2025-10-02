class Program
{
    static int Main(string[] args)
    {
        var parser = new ArgumentParser();
        var parse = parser.Parse(args);
        if (!parse.Ok)
        {
            Console.WriteLine(parse.ErrorMessage);
            return 1;
        }

        int n = parse.N;
        string mortyName = parse.MortyName;

        var morty = MortyLoader.Load(mortyName);
        if (morty == null)
        {
            Console.WriteLine($"Morty implementation '{mortyName}' not found. Example: ClassicMorty or LazyMorty");
            return 1;
        }

        var game = new Game(n, morty);
        game.RunLoop();
        return 0;
    }
}
