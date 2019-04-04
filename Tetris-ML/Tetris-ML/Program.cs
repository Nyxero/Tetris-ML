namespace Tetris_ML
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Tetris tetris = new Tetris(10, 18);
            tetris.Start();
        }
    }
}