class ClassicMorty : IMorty
{
    private int hiddenBox = 0;
    public string Name => "ClassicMorty";

    public void PrepareHide(int n, FairRandomGenerator generator)
    {
        var hmac = generator.StartGeneration(n);
        Console.WriteLine($"Morty: HMAC1={hmac}");
    }

    public (int chosenBox, string[] messages) RevealStep(int n, FairRandomGenerator generator, int rickPick)
    {
        var (mortyVal, final) = generator.RevealAfterRick(rickPick, n);
        Console.WriteLine($"Morty: Aww man, my 1st random value is {mortyVal}.");
        Console.WriteLine($"Morty: KEY1={generator.RevealKeyHex()}");
        hiddenBox = (int)final;
        var msgs = new string[] { $"Morty: So the 1st fair number is ({rickPick} + {mortyVal}) % {n} = {final}" };
        return (hiddenBox, msgs);
    }

    public (double pSwitch, double pStay) CalculateProbability(int n)
    {
        double stay = 1.0 / n;
        double sw = (n - 1.0) / n;
        return (sw, stay);
    }
}
