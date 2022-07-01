using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class WorldMap
    {
        private int width, height;
        List<GridCoordinates> gList = new List<GridCoordinates>();
        private List<GridCoordinates> wList = new List<GridCoordinates>();
        private List<string> col;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public List<GridCoordinates> GList { get { return gList; } }
        public List <GridCoordinates> WList { get { return wList; } }

        public WorldMap(string aSize, List<string> aMap)
        {
            Convert _con = new Convert(aSize);
            List<int> _coords = _con.ToConvert();

            width = _coords[0];
            height = _coords[1];
            col = aMap;
            drawWorldMap();

        }



        public void drawWorldMap()
        {
           for (int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    gList.Add(new GridCoordinates(new Vector(j , i), false));
                }
            }

           for(int i = 0;i < col.Count; i++)
            {
                drawRobotWall(col[i]);
            }
            drawRobotPath();
        }
        public void drawRobotPath()
        {
            for(int i = 0; i < gList.Count; i++)
            {
                if(!gList[i].IsValid)
                {
                    for(int j = 0; j< width; j++)
                    {
                        if ((i < (j + 1) * height - 1) && (i >= j * height))
                        {
                            gList[i].RobotPaths.Add(new RobotPath(gList[i + 1]));

                        }
                    }

                    if(i < height * width - height)
                    {
                        if(!gList[i + height].IsValid)
                        {
                            gList[i].RobotPaths.Add(new RobotPath(gList[i + height]));
                        }
                    }

                    for(int j = 0;j < width; j++)
                    {
                        if((i < (j + 1) * height) && (i > j * height))
                        {
                            gList[i].RobotPaths.Add(new RobotPath(gList[i - 1]));
                        }
                    }

                    if(i > height - 1)
                    {
                        if (!gList[i - height].IsValid)
                        {
                            gList[i].RobotPaths.Add(new RobotPath(gList[i - height]));
                        }
                    }

                }
            }

            foreach(GridCoordinates gridCoordinates in gList)
            {
                for (int j = 0; j < gridCoordinates.RobotPaths.Count; j++)
                {
                    if (gridCoordinates.RobotPaths[j].GridCoordinate.IsValid)
                    {
                        gridCoordinates.RobotPaths.Remove(gridCoordinates.RobotPaths[j]);
                    }
                }
            }
        }
        public void drawRobotWall(string _wall)
        {
            Convert _con = new Convert(_wall);
            List<int> grid = _con.ToConvert();

            for (int i = grid[1]; i < grid[1] + grid[3]; i++)
            {
                for(int j = grid[0]; j < grid[0] + grid[2]; j++ )
                {
                    int a = gList.FindIndex(x => (x.Location.A == j) && (x.Location.B == i));
                    gList[a].IsValid = true;
                }
            }

            foreach (GridCoordinates gridCoordinates in gList)
            {
                if (gridCoordinates.IsValid == true)
                {
                    wList.Add(gridCoordinates);
                }
            }
        }

        public void printWorldMap()
        {
            foreach (GridCoordinates gridCoordinates in gList)
            {
                Console.WriteLine("Robot Grid = ({0},{1} , Robot Wall = {2}", gridCoordinates.Location.A , gridCoordinates.Location.B, gridCoordinates.IsValid);
                Console.WriteLine("The Following grid contains = ");
                foreach(RobotPath robotPath in gridCoordinates.RobotPaths)
                {
                    Console.WriteLine(robotPath.GridCoordinate.Location.Coordinate);
                }
                Console.WriteLine("");
            }
        }

    }
}
