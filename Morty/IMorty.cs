interface IMorty
{
    string Name { get; }
    void PrepareHide(int n, FairRandomGenerator generator);
    (int chosenBox, string[] messages) RevealStep(int n, FairRandomGenerator generator, int rickPick);
    (double pSwitch, double pStay) CalculateProbability(int n);
}
