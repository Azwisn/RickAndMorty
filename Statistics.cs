class Statistics
{
    public int Rounds { get; private set; }
    public int RickSwitched { get; private set; }
    public int RickStayed { get; private set; }
    public int WinsWhenSwitched { get; private set; }
    public int WinsWhenStayed { get; private set; }

    public void AddRound(bool switched, bool rickWon)
    {
        Rounds++;
        if (switched)
        {
            RickSwitched++;
            if (rickWon) WinsWhenSwitched++;
        }
        else
        {
            RickStayed++;
            if (rickWon) WinsWhenStayed++;
        }
    }
}
