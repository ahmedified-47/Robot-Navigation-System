using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Vector
    {
        private int a, b;
        private double goalDistance, fPoints, gPoints;
        private Vector pNode;

        public Vector(Vector vNode)
        {
            a = vNode.A;
            b = vNode.B;
        }

        public Vector(int pa, int pb)
        {
            a = pa;
            b = pb;
        }
        public string Coordinate
        {
            get
            {
                return "(" + A + "," + B + ")";
            }
        }
        public Vector PNode
        {
            get
            {
                return pNode;
            }
            set
            {
                pNode = value; 
            }
        }
        public int A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }
        public int B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }
        public double FPoints
        {
            get
            {
                return fPoints;
            }
            set
            {
                fPoints = value;
            }
        }
        public double GPoints
        {
            get
            {
                return gPoints;
            }
            set
            {
                gPoints = value;
            }
        }
        public double GoalDistance
        {
            get
            {
                return goalDistance;
            }
            set
            {
                goalDistance = value;
            }
        }

    }
}
