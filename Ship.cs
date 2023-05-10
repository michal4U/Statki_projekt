using System;

namespace Battleship
{
    internal class Ship
    {
        public readonly string Name;
        public readonly int Size;

        public Ship(string name, int size)
        {
            if (size < 1)
                throw new ArgumentException("Ten statek jest za mały");

            Name = name;
            Size = size;
        }
    }
}