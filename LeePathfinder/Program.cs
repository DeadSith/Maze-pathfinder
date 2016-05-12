using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeePathfinderLib;
using System.Threading.Tasks;

namespace LeePathfinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var field = new int[,]
            {
                {-1, -1, 0, 0, 0},
                {-1, 0, 0, -1, 0},
                {-1, 0, 0, -1, 2},
                {1, 0, -1, -1, -1}
            };
            var pathFinder = new Pathfinder(field);
            pathFinder.Solve();
            var path = pathFinder.BackTrace();
            Console.ReadKey();
        }
    }
}
