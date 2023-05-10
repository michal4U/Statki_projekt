using System;
using System.Collections.Generic;

namespace Battleship
{
    internal class Game
    {
        private readonly Player computer;
        private readonly Board computerBoard;
        private readonly Player player;
        private readonly Board playerBoard;

        private readonly List<Ship> ships = new List<Ship>
        {
            new Ship("4-masztowiec", 4),
            new Ship("3-masztowiec", 3),
            new Ship("3-masztowiec", 3),
            new Ship("2-masztowiec", 2),
            new Ship("2-masztowiec", 2),
            new Ship("2-masztowiec", 2),
            new Ship("1-masztowiec", 1),
            new Ship("1-masztowiec", 1),
            new Ship("1-masztowiec", 1),
            new Ship("1-masztowiec", 1),
        };

        public Game()
        {
            playerBoard = new Board();
            computerBoard = new Board();

            player = new Player(playerBoard);
            computer = new Player(computerBoard) { Name = "Computer" };
        }

        public void Start()
        {
            player.SetName();

            InitializeShips();

            Console.WriteLine("Naciśnij enter aby rozpocząć gre!");
            Console.ReadLine();
            Console.Clear();

            while (player.TotalHits < computerBoard.TotalPlots && computer.TotalHits < playerBoard.TotalPlots)
            {
                computerBoard.DrawHits();

                Console.Write("Wprowadź kordynat X: ");
                var xInput = Console.ReadLine();

                Console.Write("Wprowadź kordynat Y: ");
                var yInput = Console.ReadLine();

                int x, y;

                try
                {
                    x = int.Parse(xInput);
                    y = int.Parse(yInput);
                }
                catch (Exception)
                {
                    Console.WriteLine("Złe kordynaty");
                    Console.WriteLine();
                    continue;
                }

                Console.Clear();
                var validAttack = player.Attack(x, y, computerBoard);

                if (!validAttack)
                {
                    Console.WriteLine("Już tu strzelałeś");
                    continue;
                }

                computer.AutoAttack(playerBoard);
            }

            PrintWinner();
        }

        public void InitializeShips()
        {
            Console.WriteLine("Cześć postaw swoje statki!");
            Console.WriteLine("kliknij enter aby postawić statki własnoręcznie albo -> A <- aby postawić je automatycznie");
            var input = Console.ReadLine();

            if (input.ToLower() == "a")
            {
                playerBoard.AutoPlotShips(ships);
                playerBoard.DrawBoard();
                Console.WriteLine();
            }
            else
            {
                playerBoard.ManuallyPlotShips(ships);
                Console.WriteLine();
                playerBoard.DrawBoard();
                Console.WriteLine();
            }

            computerBoard.AutoPlotShips(ships);
        }

        public void PrintWinner()
        {
            Console.Clear();
            var winnerName = player.TotalHits > computer.TotalHits ? player.Name : computer.Name;
            Console.WriteLine($"Zwycięzcą jest: {winnerName}!");
        }
    }
}