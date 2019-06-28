using System;
using System.Collections.Generic;

namespace Maze_Creator_Solver
{
    class MazeCreator
    {
        public int[,] Maze { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public MazeCreator()
        {
            this.Width = 40;
            this.Height = 30;
            this.Maze = new int[this.Width, this.Height];
        }

        public MazeCreator(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            this.Maze = new int[this.Width, this.Height];
        }

        public void SetRandomEndPoints()
        {
            Random rng = new Random();
            int xstart = rng.Next(this.Width);
            int ystart = rng.Next(this.Height);
            int xend = rng.Next(this.Width);
            int yend = rng.Next(this.Height);
            while (this.Maze[xstart, ystart] != 1 && this.Maze[xend, yend] != 1 && (xstart != xend && ystart != yend))
            {
                xstart = rng.Next(this.Width);
                ystart = rng.Next(this.Height);
                xend = rng.Next(this.Width);
                yend = rng.Next(this.Height);
            }
            this.StartX = xstart;
            this.StartY = ystart;
            this.EndX = xend;
            this.EndY = yend;
            Console.WriteLine("Start Maze: " + this.StartX + ", " + this.StartY + " End Point: " + this.EndX + " , " + this.EndY);
        }

        public void SetEndPointOnEdge()
        {
            List<Tuple<int, int>> validEndpoints = new List<Tuple<int,int>>();
            for (int i = 0 ; i < this.Width; i++)
            {
                if (this.Maze[i, 0] != 1)
                {
                    validEndpoints.Add(new Tuple<int, int>(i, 0));
                }
                if (this.Maze[i, this.Height - 1] != 1)
                {
                    validEndpoints.Add(new Tuple<int, int>(i, this.Height - 1));
                }
            }
            for (int j = 1; j < this.Height - 1; j++)
            {
                if (this.Maze[0, j] != 1)
                {
                    validEndpoints.Add(new Tuple<int, int>(0, j));
                }
                if (this.Maze[this.Width - 1, j] != 1)
                {
                    validEndpoints.Add(new Tuple<int, int>(this.Width - 1, j));
                }
            }
            Random rng = new Random();
            Tuple<int, int> start = validEndpoints[rng.Next(validEndpoints.Count)];
            Tuple<int, int> end = validEndpoints[rng.Next(validEndpoints.Count)];
            this.StartX = start.Item1;
            this.StartY = start.Item2;
            this.EndX = end.Item1;
            this.EndY = end.Item2;
        }

        public void CreateMaze()
        {
            Random rng = new Random();
            List<int[]> frontier = new List<int[]>();
            int randx = rng.Next(this.Width - 1);
            int randy = rng.Next(this.Height - 1);
            frontier.Add(new int[] { randx, randy, randx, randy });
            while (frontier.Count > 0)
            {
                int[] selected = frontier[rng.Next(frontier.Count - 1)];
                frontier.Remove(selected);
                int x = selected[2];
                int y = selected[3];
                if (this.Maze[x, y] == 0)
                {
                    this.Maze[selected[0], selected[1]] = 1;
                    this.Maze[x, y] = 1;
                    if (x >= 2 && this.Maze[x-2, y] == 0)
                    {
                        frontier.Add(new int[] { x - 1, y, x - 2, y});
                    }
                    if (x < this.Width - 2 && this.Maze[x+2, y] == 0)
                    {
                        frontier.Add(new int[] { x + 1, y, x + 2, y});
                    }
                    if (y >= 2 && this.Maze[x, y-2] == 0)
                    {
                        frontier.Add(new int[] { x, y - 1, x, y - 2 });
                    }
                    if (y < this.Height - 2)
                    {
                        frontier.Add(new int[] { x, y + 1, x, y + 2 });
                    }
                }
            }
        }

        public void PrintMaze()
        {
            this.Maze[this.StartX, this.StartY] = 4;
            this.Maze[this.EndX, this.EndY] = 4;
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    int val = this.Maze[i, j];
                    if (val == 1)
                    {
                        Console.Write("\u2588\u2588");
                    }
                    else if (val == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\u2588\u2588");
                        Console.ResetColor();
                    }
                    else if (val == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\u2588\u2588");
                        Console.ResetColor();
                    }
                    else { Console.Write("  "); }
                }
                Console.WriteLine("");
            }
        }
    }
}
