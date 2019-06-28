using System;
using System.Collections.Generic;
using System.Text;

namespace Maze_Creator_Solver
{
    class MazeSolver
    {
        private MazeCreator maze;

        public MazeSolver()
        {
            maze = new MazeCreator();
        }

        public bool SolveMaze()
        {
            bool[,] visited = new bool[maze.Width, maze.Height];
            bool[,] correctPath = new bool[maze.Width, maze.Height];
            bool solveable = RecursiveSolve(visited, correctPath, maze.StartX, maze.StartY);
            if (solveable)
            {
                for (int i = 0; i < maze.Width; i++)
                {
                    for (int j = 0; j < maze.Height; j++)
                    {
                        if (correctPath[i, j] == true)
                        {
                            maze.Maze[i, j] = 3;
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
                if (x == maze.EndX && y == maze.EndY)
                {
                    return true;
                }
                if (maze.Maze[x, y] == 0 || visited[x, y] == true)
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
                if (x < maze.Width - 1)
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
                if (y < maze.Height - 1)
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

        public static void Main()
        {
            MazeSolver maze = new MazeSolver();
            maze.maze.CreateMaze();
            //maze.maze.SetRandomEndPoints();
            maze.maze.SetEndPointOnEdge();
            maze.maze.PrintMaze();
            bool solveable = maze.SolveMaze();
            if (solveable)
            {
                maze.maze.PrintMaze();
            }
            else
            {
                Console.WriteLine("Maze not solveable");
            }
        }
    }
}
