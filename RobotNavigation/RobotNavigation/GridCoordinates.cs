using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class GridCoordinates
    {
        private bool isValid;
        private Vector location;
        private List<RobotPath> robotPaths = new List<RobotPath>();
        public Vector Location => location;
        public List<RobotPath> RobotPaths
        {
            get
            {
                return robotPaths;
            }
            set
            {
                robotPaths = value;
            }
        }
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }
        public GridCoordinates(Vector val, bool isVal)
        {
            location = val;
            isValid = isVal;
        }
    }
}
