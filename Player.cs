using System;

namespace Battleship
{
    internal class Player
    {
        private readonly Board board;
        private readonly Random random = new Random();

        public Player(Board board)
        {
            this.board = board;
        }

        public string Name { get; set; }
        public int TotalHits { get; set; }

        public string SetName()
        {
            while (true)
            {
                Console.Write("Podaj swoj nick: ");
                Name = Console.ReadLine();

                if (Name.Length == 0)
                {
                    Console.WriteLine("Podany nick jest za krótki");
                    continue;
                }

                break;
            }

            return Name;
        }

        public bool Attack(int x, int y, Board board)
        {
            if (!board.IsValidCoordinate(x, y) || board.IsPlotHit(x, y))
                return false;

            var fire = board.Fire(x, y);

            if (fire)
            {
                Console.WriteLine("Statek został trafiony");
                TotalHits++;
            }
            else
            {
                Console.WriteLine($"{Name} nie trafił!...ufff");
            }

            Console.WriteLine();
            board.DrawHits();
            Console.ReadLine();
            Console.Clear();

            return true;
        }

        public void AutoAttack(Board board)
        {
            while (true)
            {
                var x = random.Next(0, 9);
                var y = random.Next(0, 9);

                var valid = Attack(x, y, board);

                if (valid)
                    break;
            }
        }
    }
}