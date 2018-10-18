using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Geometry;
using DynamoETABS.Definitions;
using Autodesk.DesignScript.Runtime;
using DynamoServices;


namespace DynamoETABS.Structure
{
    public class Joint
    {
        //Base Point From Dynamo
        internal Point Pt { get; set; }
        //Joint Coordinates
        internal double X { get; set; }
        internal double Y { get; set; }
        internal double Z { get; set; }
        //Joint Loads
        internal Loads loads { get; set; }

        //Create a New Joint
        public static Joint ByPoints (Point Point)
        {
            Joint Jt = new Joint(Point);
            return Jt;
        }

        public static Joint AddJointLoad(Joint Joint,Loads Loads)
        {
            Joint.loads = Loads;
            return Joint;
        }

        //Constructors
        internal Joint() { }
        internal Joint (Point pt)
        {

            Pt = pt;
            X = pt.X;
            Y = pt.Y;
            Z = pt.Z;

        }
       
    }
}
