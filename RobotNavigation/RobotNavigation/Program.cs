using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Program
    {
        static void Main(string[] args)
        {
            ResourceInitialiser Init = new ResourceInitialiser(@"RobotNav-test.txt");
            Init.populateData();
            WorldMap worldMap = new WorldMap(Init.Map, Init.Col);
            Robot rb = new Robot(Init.StartState, Init.EndState, worldMap);

            Console.WriteLine("Mode: " + args[0]); 
            switch (args[0].ToUpper())
            {
                case "DFS":
                    Console.WriteLine(rb.DepthFirstSesrch());
                    break;
                case "BFS":
                    Console.WriteLine(rb.BreadthFirstSearch());
                    break;
                case "GBFS":
                    Console.WriteLine(rb.GreedyBestFirstSearch());
                    break;
                case "ASTAR":
                    Console.WriteLine(rb.AStarSearch());
                    break;
                case "DJ":
                    Console.WriteLine(rb.DijkstraSearch());
                    break;
                default:
                    Console.WriteLine(rb.ToString());
                    break;
            
            }
            Console.ReadLine();
        }
    }
}
