class ArgParseResult
{
    public bool Ok { get; set; }
    public string ErrorMessage { get; set; } = "";
    public int N { get; set; }
    public string MortyName { get; set; } = "";
}

class ArgumentParser
{
    public ArgParseResult Parse(string[] args)
    {
        var r = new ArgParseResult();
        if (args == null || args.Length == 0)
        {
            r.Ok = false;
            r.ErrorMessage = "Missing arguments. Usage example: dotnet run -- 3 ClassicMorty";
            return r;
        }
        if (args.Length < 2)
        {
            r.Ok = false;
            r.ErrorMessage = "Please provide two arguments: <number_of_boxes> <MortyName>. Example: dotnet run -- 3 ClassicMorty";
            return r;
        }
        if (!int.TryParse(args[0], out int n))
        {
            r.Ok = false;
            r.ErrorMessage = "Invalid number of boxes. Must be integer > 2. Example: dotnet run -- 3 ClassicMorty";
            return r;
        }
        if (n <= 2)
        {
            r.Ok = false;
            r.ErrorMessage = "Number of boxes must be greater than 2. Example: dotnet run -- 3 ClassicMorty";
            return r;
        }
        r.Ok = true;
        r.N = n;
        r.MortyName = args[1];
        return r;
    }
}
