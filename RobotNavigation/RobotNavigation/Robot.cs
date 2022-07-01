using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace RobotNavigation
{
    public class Robot
    {
        private Vector presentNode, goalNode;
        private WorldMap rMap;
        private UI u = new UI();

        public Vector PresentNode => presentNode;

        public Robot(string startState, string endState, WorldMap aMap)
        {
            Convert con = new Convert(startState);
            List<int> coords = con.ToConvert();

            presentNode = new Vector(coords[0] , coords[1]);
            // Console.WriteLine("s: " + coords[0] + ", " + coords[1]);
            con = new Convert(endState);
            coords = con.ToConvert();
            goalNode = new Vector(coords[0], coords[1]);
            // Console.WriteLine("s: " + coords[0] + ", " + coords[1]);
            rMap = aMap;

           
        }

        public void notification()
        {
            Console.WriteLine("Current Position ({0},{1})", presentNode.A , presentNode.B);
            foreach(GridCoordinates gridCoordinates in rMap.GList) // problem maybe
            {
                if(presentNode.A == gridCoordinates.Location.A && presentNode.B == gridCoordinates.Location.B)
                {
                    Console.WriteLine("Possible next moves:");
                    foreach(RobotPath path in gridCoordinates.RobotPaths)
                    {
                        Console.WriteLine("({0},{1})", path.GridCoordinate.Location.A , path.GridCoordinate.Location.B); // Possible error
                    }
                }
            }
            Console.WriteLine("Goal: ({0},{1})" , goalNode.A , goalNode.B);
        }

        public string Moveup()
        {
            return "up";
        }
        public string MoveDown()
        {
            return "down";
        }

        public string MoveLeft()
        {
            return "left";
        }

        public string MoveRight()
        {
            return "right";
        }

        public string DepthFirstSesrch()
        {
            if(presentNode.A == goalNode.A && presentNode.B == goalNode.B)
            {
                return "Start Position is the goal state";
            }
            else
            {
                //Initialize Data Structure
                Stack<Vector> opened = new Stack<Vector>();
                List<Vector> visited = new List<Vector>();

                Vector vNode; //expanding node

                opened.Push(presentNode); // push the start position of robot in stack

                while (opened.Count != 0)
                {
                    vNode = opened.Pop();  // it visits a node and pop it out of the stack
                    visited.Add(vNode); // visit a node to expand it and it in the visited list
                     
                    Debug.WriteLine("Expanding: " + vNode.Coordinate);
                    u.Draw(presentNode, goalNode,  vNode, rMap.WList, rMap.Width, rMap.Height); // UI function to draw the map when program runs
                    Thread.Sleep(200);

                    foreach(GridCoordinates coord in rMap.GList)
                    {
                        if(vNode.A == coord.Location.A && vNode.B == coord.Location.B) // checks if the grid coordinate expanded is with in the map
                        {
                            if(coord.RobotPaths.Count != 0) // checks if nodes on the sides are available
                            {
                                foreach(RobotPath robotPath in coord.RobotPaths) 
                                {
                                    if((!visited.Any(a => a.A == robotPath.GridCoordinate.Location.A && a.B == robotPath.GridCoordinate.Location.B)) && !opened.Any(a => a.A == robotPath.GridCoordinate.Location.A && a.B == robotPath.GridCoordinate.Location.B)) // looks for repeated states
                                    {
                                        robotPath.GridCoordinate.Location.PNode = new Vector(vNode);
                                        Debug.WriteLine(robotPath.GridCoordinate.Location.Coordinate);
                                        opened.Push(robotPath.GridCoordinate.Location); // push the nodes on the side to the frontier
                                    } 
                                }
                            }

                            if(vNode.A == goalNode.A && vNode.B == goalNode.B)
                            {
                                return produceSolution("DFS", presentNode, goalNode, visited);
                            }
                        }
                    }
                }
                return "No solution!";
            }
        }

        public string BreadthFirstSearch()
        {
            if(presentNode.A == goalNode.A && presentNode.B == goalNode.B)
            {
                return "The solution is the start position";
            }
            else
            {
                //Initialize Data Structure
                Queue<Vector> opened = new Queue<Vector>();
                List<Vector> visited = new List<Vector>();

                Vector vNode; //expanding node
                opened.Enqueue(PresentNode); // intialise a queue for the present node

                while(opened.Count != 0)
                {
                    vNode = opened.Dequeue(); // expand the first node of the queue
                    visited.Add(vNode);

                    u.Draw(presentNode, goalNode, vNode, rMap.WList, rMap.Width, rMap.Height);

                    Thread.Sleep(200);

                    foreach(GridCoordinates gridCoordinates in rMap.GList)
                    {
                        if(vNode.A == gridCoordinates.Location.A && vNode.B == gridCoordinates.Location.B) // checks if the grid coordinate expanded is with in the map
                        {
                            if(gridCoordinates.RobotPaths.Count != 0) // checks if nodes on the sides are available
                            {
                                foreach(RobotPath robotPath in gridCoordinates.RobotPaths)
                                {
                                    if((!visited.Any(a => a.A == robotPath.GridCoordinate.Location.A && a.B == robotPath.GridCoordinate.Location.B)) && !opened.Any(a => a.A == robotPath.GridCoordinate.Location.A && a.B == robotPath.GridCoordinate.Location.B))// check for repeated state
                                    {
                                        robotPath.GridCoordinate.Location.PNode = new Vector(vNode);

                                        opened.Enqueue(robotPath.GridCoordinate.Location); // available enqueue paths to the frontier list

                                    }
                                }
                            }

                            if(vNode.A == goalNode.A && vNode.B == goalNode.B) // Checks if there is a solution
                            {
                                return produceSolution("BFS", presentNode, goalNode, visited);
                            }

                        }
                    }
                }

                return "No solution";
            }

        }

        public string GreedyBestFirstSearch()
        {
            if(presentNode.A == goalNode.A && presentNode.B == goalNode.B)
            {
                return "Start state is the goal state!";
            }
            else
            {
                //Initialize Data Structure
                List<Vector> opened = new List<Vector>();
                List<Vector> visited = new List<Vector>();

                Vector vNode; // expanding node
                opened.Add(presentNode);

                while(opened.Count != 0)
                {
                    opened = opened.OrderBy(a => a.GoalDistance).ToList(); // sort the list with regard to goal distance
                    vNode = opened.First(); // expand the first node of the sorted list
                    opened.Remove(opened.First());
                    visited.Add(vNode);

                    u.Draw(presentNode, goalNode, vNode, rMap.WList, rMap.Width, rMap.Height);


                    Thread.Sleep(100);

                    foreach(GridCoordinates gridCoordinates in rMap.GList)
                    {
                        if(vNode.A == gridCoordinates.Location.A && vNode.A == gridCoordinates.Location.B) // checks if the grid coordinate expanded is with in the map
                        {
                            if(gridCoordinates.RobotPaths.Count != 0)  // checks if nodes on the sides are available
                            {
                                foreach(RobotPath robotPath in gridCoordinates.RobotPaths) 
                                {
                                    if(!visited.Exists(a => a.A == robotPath.GridCoordinate.Location.A && a.B == robotPath.GridCoordinate.Location.B)) // check for repeated state
                                    {
                                        robotPath.GridCoordinate.Location.PNode = new Vector(vNode);
                                        // calculating h(n) value
                                        robotPath.GridCoordinate.Location.GoalDistance = Math.Sqrt(Math.Pow(goalNode.A - robotPath.GridCoordinate.Location.A, 2) + Math.Pow(goalNode.B - robotPath.GridCoordinate.Location.B, 2));

                                        opened.Add(robotPath.GridCoordinate.Location); // add nodes which are adjacent to the list
                                    }
                                }
                            }

                            if(vNode.A == goalNode.A && vNode.B == goalNode.B) // checks if there is a solution
                            {
                                return produceSolution("GBFS", presentNode, goalNode, visited);
                            }
                        }
                    }
                }
                return "No solution"; // if there is no solution
            }
        }


        public string AStarSearch()
        {
            if(presentNode.A == goalNode.A && presentNode.B == goalNode.B) // checks if agent is already on the goal state
            {
                return "Start state is the goal state";
            }
            else
            {
                // initialize data structure
                List<Vector> opened = new List<Vector>();
                List<Vector> visited = new List<Vector>();

                Vector vNode; // expanding node

                opened.Add(presentNode); // puts the start position in list
                presentNode.GPoints = 0; // Initalize goal points to zero

                while(opened.Count != 0)
                {
                    opened = opened.OrderBy(a => a.FPoints).ToList(); // sort the list with the help of f(n) value

                    vNode = opened.First(); // expand the first node of the sorted list
                    opened.Remove(opened.First());
                    visited.Add(vNode); // add the expanded node to the list of visited nodes

                    u.Draw(presentNode, goalNode, vNode, rMap.WList,  rMap.Width, rMap.Height);

                    Thread.Sleep(200);

                    foreach(GridCoordinates gridCoordinates in rMap.GList)
                    {
                        if(vNode.A == gridCoordinates.Location.A  && vNode.B == gridCoordinates.Location.B) // checks if the grid coordinate expanded is with in the map
                        {
                            if(gridCoordinates.RobotPaths.Count != 0) // checks if nodes on the sides are available
                            {
                                foreach(RobotPath robotPath in gridCoordinates.RobotPaths)
                                {
                                    if((!visited.Any(a => a.A == robotPath.GridCoordinate.Location.A && a.B == robotPath.GridCoordinate.Location.B)) && !opened.Any(a => a.A == robotPath.GridCoordinate.Location.A && a.B == robotPath.GridCoordinate.Location.B)) // check for repeated states
                                    {
                                        robotPath.GridCoordinate.Location.PNode = new Vector(vNode);
                                        //calculate g(n) value value from start to current node
                                        robotPath.GridCoordinate.Location.GPoints = vNode.GPoints + 1;
                                        // calculating f(n) value
                                        robotPath.GridCoordinate.Location.FPoints = robotPath.GridCoordinate.Location.GPoints + Math.Sqrt(Math.Pow(goalNode.A - robotPath.GridCoordinate.Location.A, 2) + Math.Pow(goalNode.B - robotPath.GridCoordinate.Location.B, 2));
                                        opened.Add(robotPath.GridCoordinate.Location); // add nodes on the side to the list
                                    }
                                }
                            }
                            if (vNode.A == goalNode.A && vNode.B == goalNode.B) // checks if there is a solution
                            {
                                return produceSolution("AStar", presentNode, goalNode, visited);
                            }
                        }
                    }
                }
                return "No solution"; // if there is no solution
            }
        }

        public string DijkstraSearch()
        {
            if (presentNode.A == goalNode.A && presentNode.B == goalNode.B) // checks if agent is already at start position
            {
                return "Start state is the goal state";
            }
            else
            {
                // initialize data structure
                List<Vector> opened = new List<Vector>();
                List<Vector> visited = new List<Vector>();

                Vector vNode;

                opened.Add(presentNode);
                presentNode.GPoints = 0;

                while (opened.Count != 0)
                {
                    opened = opened.OrderBy(a => a.FPoints).ToList(); // sort the list with the help of f(n) value

                    vNode = opened.First(); // expand the first node of the sorted list
                    opened.Remove(opened.First());
                    visited.Add(vNode); // add the expanded node to the list of visited nodes

                    u.Draw(presentNode, goalNode, vNode, rMap.WList, rMap.Width, rMap.Height);

                    Thread.Sleep(200);

                    foreach (GridCoordinates gridCoordinates in rMap.GList)
                    {
                        if (vNode.A == gridCoordinates.Location.A && vNode.B == gridCoordinates.Location.B) // checks if the grid coordinate expanded is with in the map
                        {
                            if (gridCoordinates.RobotPaths.Count != 0) // checks if nodes on the sides are available
                            {
                                foreach (RobotPath robotPath in gridCoordinates.RobotPaths)
                                {
                                    if ((!visited.Any(a => a.A == robotPath.GridCoordinate.Location.A && a.B == robotPath.GridCoordinate.Location.B)) && !opened.Any(a => a.A == robotPath.GridCoordinate.Location.A && a.B == robotPath.GridCoordinate.Location.B)) // check for repeated states
                                    {
                                        robotPath.GridCoordinate.Location.PNode = new Vector(vNode);
                                        //calculate g(n) value value from start to current node
                                        robotPath.GridCoordinate.Location.GPoints = vNode.GPoints + 1;
                                        // calculating f(n) value
                                        //robotPath.GridCoordinate.Location.FPoints = robotPath.GridCoordinate.Location.GPoints + Math.Sqrt(Math.Pow(goalNode.A - robotPath.GridCoordinate.Location.A, 2) + Math.Pow(goalNode.B - robotPath.GridCoordinate.Location.B, 2));
                                        opened.Add(robotPath.GridCoordinate.Location); // add nodes on the side to the list
                                    }
                                }
                            }
                            if (vNode.A == goalNode.A && vNode.B == goalNode.B)
                            {
                                return produceSolution("Dijkstra", presentNode, goalNode, visited);
                            }
                        }
                    }
                }
                return "No solution";
            }
        }

        public string produceSolution(string searchType, Vector aStart, Vector aChild, List<Vector> expand)
        {
            string sol = "";
            List<Vector> robotDir = new List<Vector>();
            List<string> robotAction = new List<string>();

            expand.Reverse();
            foreach(Vector v in expand)
            {
                if((v.A == aChild.A && v.B == aChild.B))
                {
                    robotDir.Add(v);
                }
                if(robotDir.Count != 0)
                {
                    if((robotDir.Last().PNode.A == v.A) && (robotDir.Last().PNode.B == v.B))
                    {
                        robotDir.Add(v);
                    }
                }
            }
            robotDir.Reverse();
            for(int i = 0; i < robotDir.Count(); i++)
            {
                if(i == robotDir.Count() - 1)
                {
                    break;
                }

                if(robotDir[i + 1].A == robotDir[i].A + 1)
                {
                    robotAction.Add(MoveRight());
                }

                if(robotDir[i + 1].A == robotDir[i].A - 1)
                {
                    robotAction.Add(MoveLeft());
                }

                if(robotDir[i + 1].B == robotDir[i].B + 1)
                {
                    robotAction.Add(MoveDown());
                }
                if(robotDir[i + 1].B == robotDir[i].B - 1)
                {
                    robotAction.Add(Moveup());
                }

            }

            foreach(string str in robotAction)
            {
                sol = sol + str + "; ";
            }
           
            u.DrawPath(presentNode, goalNode, robotDir, rMap.WList, rMap.Width, rMap.Height);

            return searchType + " " + expand.Count() + " " + sol;

        }
        


    }
}
