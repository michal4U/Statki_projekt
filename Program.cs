using System;
using System.Collections.Generic;

namespace Battleship
{
    internal class Battleships
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Witam w mojej grze w statki!");
            Console.WriteLine();
            new Game().Start();
        }
    }
}