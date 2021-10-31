namespace RPSLSGAMEver4
{
    public interface IMachine
    {
        char MachinePressedkey { get; set; }
        int MachinePoint { get; set; }
        string MachineChoosedGameItem { get; set; }
        char GetMachineKey(Board game);
        string GetChoosedMachineGameItem(Board game);
        int GetMachinePoint();
    }
}