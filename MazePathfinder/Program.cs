using MazePathfinderLib;
using System;

namespace MazePathfinder
{
    internal class Program//Todo: add legend
    {
        private static void Main(string[] args)
        {
            var field = new int[,]
            {
                {-1, -1, 0, 0, 0},
                {-1, 0, 0, -1, 0},
                {-1, 0, 0, -1, 2},
                {1, 0, -1, -1, -1},
                {0, 0, 0, 0, 0 }
            };
            var pathFinder = new AStarPathfinder(field);
            if (!pathFinder.Solve())
            {
                Console.WriteLine("Error. There is no way to reach end from start");
            }
            var lee = new LeePathfinder(field);//Be careful the field is passed by referrence and changed in constructor
            if (lee.Solve())
            {
                var path = lee.BackTrace();
            }
            else
            {
                Console.WriteLine("Error. There is no way to reach end from start");
            }
            Console.ReadKey();
        }
    }
}