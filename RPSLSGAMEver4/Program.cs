namespace RPSLSGAMEver4
{
    class Program
    {
        static void Main(string[] args)
        {
            Board game = new Board();
            Player player = new Player();
            Machine machine = new Machine();

            game.Initialize(player,machine,game);

        }
    }
}
