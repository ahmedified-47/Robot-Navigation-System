using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class RobotPath
    {
        GridCoordinates gridCoordinate;
        public RobotPath(GridCoordinates coordinates)
        {
            gridCoordinate = coordinates;
        }
        public GridCoordinates GridCoordinate
        {
            get { return gridCoordinate; }
            set { gridCoordinate = value; }
        }
    }
}
