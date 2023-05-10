using System;
using System.Collections.Generic;
using static System.String;
namespace Battleship
{
    internal class Board
    {
        private readonly string[,] board = new string[10, 10];
        public int TotalPlots { get; set; }

        public void Plot(int x, int y)
        {
            board[x, y] = "S";
            TotalPlots++;
        }

        public bool IsValidCoordinate(int x, int y)
        {
            return x <= 9 && x >= 0 && y >= 0 && y <= 9;
        }

        public bool IsPlotAvailable(int x, int y)
        {
            return IsValidCoordinate(x, y) && IsNullOrEmpty(board[x, y]);
        }

        public bool IsPlotHit(int x, int y)
        {
            string plot = board[x, y];
            return plot == "H" || plot == "M";
        }

        public bool Fire(int x, int y)
        {
            string plot = board[x, y];

            if (plot == "S")
            {
                board[x, y] = "H";
                return true;
            }
            else
            {
                board[x, y] = "M";
                return false;
            }
        }

        public void DrawBoard(bool onlyShowHits = false)
        {
            Console.WriteLine();
            Console.WriteLine("  | 0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("--|--------------------");

            for (var y = 0; y <= 9; y++)
            {
                Console.Write($"{y} |");

                for (var x = 0; x <= 9; x++)
                {
                    var plot = board[x, y];

                    if (IsNullOrEmpty(plot))
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        if (onlyShowHits && plot == "H" || plot == "M")
                            Console.Write(" " + plot);

                        else if (onlyShowHits == false)
                            Console.Write(" " + plot);

                        else
                            Console.Write("  ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void DrawHits()
        {
            DrawBoard(true);
        }

        public void ManuallyPlotShips(List<Ship> ships)
        {
            DrawBoard();

            foreach (var ship in ships)
                while (true)
                {
                    Console.WriteLine($"Postaw swój {ship.Name}");

                    Console.Write("Wprowadź kordynat X: ");
                    var xInput = Console.ReadLine();

                    Console.Write("Wprowadź kordynat Y: ");
                    var yInput = Console.ReadLine();

                    Console.Write("h / v (poziomo / pionowo): ");
                    var direction = Console.ReadLine().ToLower();

                    int x, y;

                    try
                    {
                        x = int.Parse(xInput);
                        y = int.Parse(yInput);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Złe kordynaty.");
                        Console.WriteLine();
                        continue;
                    }

                    if (direction != "v" && direction != "h")
                    {
                        Console.WriteLine(
                            "h / v (poziomo / pionowo): ");
                        Console.WriteLine();
                        continue;
                    }

                    var haShipPlot = PlotShip(x, y, direction, ship.Size);

                    if (haShipPlot)
                    {
                        Console.WriteLine($"Postawiono twój {ship.Name}");
                        break;
                    }

                    Console.Clear();
                    Console.WriteLine($"{ship.Name} nie może zostać tu postawiony");
                }
        }

        public void AutoPlotShips(List<Ship> ships)
        {
            var random = new Random((int)DateTime.Now.Ticks);

            foreach (var ship in ships)
                while (true)
                {
                    var x = random.Next(0, 10);
                    var y = random.Next(0, 10);
                    var direction = random.Next(0, 10) > 4 ? "h" : "v";

                    var haShipPlot = PlotShip(x, y, direction, ship.Size);

                    if (haShipPlot)
                        break;
                }
        }

        public bool PlotShip(int x, int y, string direction, int shipSize)
        {
            var plotsList = new List<List<int>>();

            for (var size = 0; size < shipSize; size++)
            {
                var shipX = x;
                var shipY = y;

                if (direction == "h")
                    shipX += size;
                else
                    shipY += size;

                if (!IsPlotAvailable(shipX, shipY))
                    break;
                if (IsPlotAdjacent(shipX, shipY))
                    return false;

                plotsList.Add(new List<int> { shipX, shipY });
            }

            if (plotsList.Count != shipSize)
                return false;

            foreach (var plot in plotsList)
                Plot(plot[0], plot[1]);

            return true;
        }
        private bool IsPlotAdjacent(int x, int y)
        {
            int[] xOffsets = { -1, 0, 1, 0, -1, -1, 1, 1 };
            int[] yOffsets = { 0, -1, 0, 1, -1, 1, -1, 1 };

            for (int i = 0; i < 8; i++)
            {
                int adjacentX = x + xOffsets[i];
                int adjacentY = y + yOffsets[i];

                if (IsValidCoordinate(adjacentX, adjacentY) && !IsPlotAvailable(adjacentX, adjacentY))
                    return true;
            }

            return false;
        }

    }
}