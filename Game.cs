class Game
{
    private readonly int n;
    private readonly IMorty morty;
    private readonly Statistics stats = new Statistics();
    private readonly FairRandomGenerator generator = new FairRandomGenerator();

    public Game(int n, IMorty morty)
    {
        this.n = n;
        this.morty = morty;
    }

    public void RunLoop()
    {
        Console.WriteLine($"Morty: Oh geez, Rick, I'm gonna hide your portal gun in one of the {n} boxes, okay?");
        while (true)
        {
            PlayRound();
            Console.WriteLine("Morty: D-do you wanna play another round (y/n)?");
            var ans = Console.ReadLine()?.Trim().ToLower();
            if (ans != "y") break;
        }
        var probs = morty.CalculateProbability(n);
        ConsoleTablePrinter.PrintStatistics(stats, probs);
    }

    private void PlayRound()
    {
        morty.PrepareHide(n, generator);

        int rickValue = AskInt($"Morty: Rick, enter your number [0,{n}) so you don’t whine later that I cheated, alright?");
        Console.WriteLine($"Morty: Okay, okay, I hid the gun. What’s your guess [0,{n})?");
        int rickGuess = AskInt("Your guess:");

        int other = PickOtherBox(rickGuess);
        Console.WriteLine($"Morty: I'm keeping box {rickGuess} and box {other}.");
        bool switched = AskSwitch(rickGuess, other);
        int finalChoice = switched ? other : rickGuess;

        var (hiddenBox, msgs) = morty.RevealStep(n, generator, rickValue);
        foreach (var m in msgs) Console.WriteLine(m);
        Console.WriteLine($"Morty: The portal gun is in the box {hiddenBox}.");

        bool rickWon = (finalChoice == hiddenBox);
        Console.WriteLine(rickWon ? "Morty: Aww man, you won, Rick." : "Morty: Aww man, you lost, Rick.");
        stats.AddRound(switched, rickWon);
    }


    private int AskInt(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var s = Console.ReadLine();
            if (int.TryParse(s, out int v) && v >= 0 && v < n) return v;
            Console.WriteLine($"Please enter integer in [0,{n}). Example: 0");
        }
    }

    private bool AskSwitch(int rickGuess, int other)
    {
        Console.WriteLine($"Morty: You can switch your box (enter {other}), or stick with it (enter {rickGuess}).");
        while (true)
        {
            var s = Console.ReadLine();
            if (int.TryParse(s, out int v))
            {
                if (v == other) return true;
                if (v == rickGuess) return false;
            }
            Console.WriteLine($"Enter {other} to switch, {rickGuess} to stay.");
        }
    }


    private int PickOtherBox(int rickPick)
    {
        for (int i = 0; i < n; i++)
        {
            if (i != rickPick) return i;
        }
        return rickPick;
    }
}
