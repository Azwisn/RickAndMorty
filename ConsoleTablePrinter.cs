using ConsoleTables;

static class ConsoleTablePrinter
{
    public static void PrintStatistics(Statistics s, (double pSwitch, double pStay) probs)
    {
        var table = new ConsoleTable("Game results", "Rick switched", "Rick stayed");

        table.AddRow("Rounds", s.RickSwitched, s.RickStayed);
        table.AddRow("Wins", s.WinsWhenSwitched, s.WinsWhenStayed);
        table.AddRow("P (estimate)", s.RickSwitched == 0 ? "?" : (s.WinsWhenSwitched / (double)Math.Max(1, s.RickSwitched)).ToString("F3"), s.RickStayed == 0 ? "?" : (s.WinsWhenStayed / (double)Math.Max(1, s.RickStayed)).ToString("F3"));
        table.AddRow("P (exact)", probs.pSwitch.ToString("F3"), probs.pStay.ToString("F3"));
        Console.WriteLine();
        table.Write(Format.Alternative);
        Console.WriteLine();
    }
}
