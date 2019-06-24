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

        /*
         * Currently selectes any random array index as start of finish
         * Can be updated to select only indices at border
         */
        public void CreateEndPoints()
        {
            Random rng = new Random();
            int xstart = rng.Next(this.Width);
            int ystart = rng.Next(this.Height);
            int xend = rng.Next(this.Width);
            int yend = rng.Next(this.Height);
            while (this.Maze[xstart, ystart] != 1 && this.Maze[xend, yend] != 1 && (xstart == xend && ystart == yend))
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

        public bool SolveMaze()
        {
            bool[,] visited = new bool[this.Width, this.Height];
            bool[,] correctPath = new bool[this.Width, this.Height];
            bool solveable = RecursiveSolve(visited, correctPath, this.StartX, this.StartY);
            if (solveable)
            {
                for (int i=0; i<this.Width; i++)
                {
                    for (int j=0; j<this.Height; j++)
                    {
                        if (correctPath[i, j] == true)
                        {
                            this.Maze[i, j] = 3;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool RecursiveSolve(bool[,] visited, bool[,] correctPath, int x, int y)
        {
            try
            {
                if (x == this.EndX && y == this.EndY)
                {
                    return true;
                }
                if (this.Maze[x, y] == 0 || visited[x, y] == true)
                {
                    return false;
                }
                visited[x, y] = true;
                if (x > 0)
                {
                    if (RecursiveSolve(visited, correctPath, x - 1, y))
                    {
                        correctPath[x, y] = true;
                        return true;
                    }
                }
                if (x < this.Width - 1)
                {
                    if (RecursiveSolve(visited, correctPath, x + 1, y))
                    {
                        correctPath[x, y] = true;
                        return true;

                    }
                }
                if (y > 0)
                {
                    if (RecursiveSolve(visited, correctPath, x, y - 1))
                    {
                        correctPath[x, y] = true;
                        return true;
                    }
                }
                if (y < this.Height - 1)
                {
                    if (RecursiveSolve(visited, correctPath, x, y + 1))
                    {
                        correctPath[x, y] = true;
                        return true;
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Error at x=" + x + " y=" + y);
            }
            return false;
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

        public static void Main()
        {
            MazeCreator maze = new MazeCreator();
            maze.CreateMaze();
            maze.CreateEndPoints();
            maze.PrintMaze();
            bool solveable = maze.SolveMaze();
            if (solveable)
            {
                maze.PrintMaze();
            }
            else
            {
                Console.WriteLine("Maze not solveable");
            }
        }
    }
}
