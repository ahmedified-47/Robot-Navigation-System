using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class UI
    {
        public UI()
        {}
          
        public void Draw(Vector aStart , Vector aEnd, Vector aNodeReached , List<GridCoordinates>wallList, int aWidth, int aHeight)
        {
            Console.Clear();
            bool isDrawn = false;
            for (int i = 0; i < aWidth; i++)
            {
                for (int j = 0; j < aHeight; j++)
                {
                    if (aStart.A == j && aStart.B == i)
                    {
                        Console.Write("|i");
                        continue;
                    }
                    if (aEnd.A == j && aEnd.B == i)
                    {
                        Console.Write("|g");
                        continue;
                    }
                    if(aNodeReached.A == j && aNodeReached.B == i)
                    {
                        Console.Write("|x");
                        continue;
                    }

                    foreach(GridCoordinates index in wallList)
                    {
                        if(index.Location.A == j && index.Location.B == i && index.IsValid)
                        {
                            Console.Write("|w");
                            isDrawn = true;
                            break;
                        }
                        isDrawn = false;
                    }

                    if (isDrawn == false)
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine("|");
            }

            if(aNodeReached.A == aEnd.A && aNodeReached.B == aEnd.B)
            {
                Console.WriteLine("\nSolution on ({0},{1}): ", aNodeReached.A, aNodeReached.B);
            }
            else
            {
                Console.WriteLine("Expanding on ({0},{1}): " , aNodeReached.A , aNodeReached.B);
            }
        }

         public void DrawPath(Vector aStart, Vector aEnd, List<Vector> aPath, List<GridCoordinates> wallList, int aWidth, int aHeight)
        {
            Console.Clear();
            bool isDrawn = false;
            for(int i = 0; i < aWidth; i++)
            {
                for(int j = 0; j < aHeight; j++)
                {
                    if(aStart.A ==j && aStart.B == i)
                    {
                        Console.Write("|i");
                        continue;
                    }

                    if (aEnd.A ==j && aEnd.B == i)
                    {
                        Console.Write("|g");
                        continue;
                    }
                    if(aPath.Any(a => a.A == j && a.B == i))
                    {
                        Console.Write("|x");
                        continue;
                    }

                    foreach(GridCoordinates gridCoordinates in wallList)
                    {
                        if((gridCoordinates.IsValid == true) && (gridCoordinates.Location.A == j) && gridCoordinates.Location.B == i)
                        {
                            Console.Write("|w");
                            isDrawn = true;
                            break;
                        }
                        isDrawn = false;
                    }

                    if (isDrawn == false)
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine("|");
            }
            Console.WriteLine();
        }

    }
}
