using System;
using System.Collections.Generic;
using System.Threading;

namespace Tetris_ML
{
    class Program
    {
        static void Main(string[] args)
        {
            Tetris tetris = new Tetris(10, 18);
            tetris.Start();
        }
    }
}
