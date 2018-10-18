using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Autodesk.DesignScript.Geometry;
using DynamoETABS.Definitions;
using Autodesk.DesignScript.Runtime;
using DynamoServices;


namespace DynamoETABS.Structure
{
    public class Joint : Element
    {
        //Base Point From Dynamo
        internal Point Pt { get; set; }
        //Joint Coordinates
        internal double X { get; set; }
        internal double Y { get; set; }
        internal double Z { get; set; }
        //Joint Loads
        internal Loads loads { get; set; }
        //ID 
        internal int ID { get; set; }
        //Label
        internal string Label { get; set; }


        //Create a New Joint
        [RegisterForTrace]
        public static Joint ByPoints (Point Point,Loads Load)
        {
            Joint Jt;
            JointID JtID = null;

            //Check if Joint already exists
            Dictionary<string, ISerializable> getJoints = ProtoCore.Lang.TraceUtils.GetObjectFromTLS();
            foreach (var k in getJoints.Keys)
            {
                JtID = getJoints[k] as JointID;
            }

            if (JtID == null)
            {
                //If the Joint doesnt exist, create new joint and assign ID
               Jt = new Joint(Point,Load);
               Jt.Label = string.Format("Joint_{0}", Jt.ID.ToString());

            }
            else
            {
                //If the Joint exists, update attributes of existing Joint element
                Jt = JointCounter.GetJointByID(JtID.IntID);

                Jt.Pt = Point;
                Jt.X = Point.X;
                Jt.Y = Point.Y;
                Jt.Z = Point.Z;
                Jt.loads = Load;
                
            }


            //Add Joint element to TLS
            Dictionary<string, ISerializable> joints = new Dictionary<string, ISerializable>();
            joints.Add(TRACE_ID, new JointID { IntID = Jt.ID });
            ProtoCore.Lang.TraceUtils.SetObjectToTLS(joints);


            return Jt;

            
        }

        
        //Constructors
        internal Joint() { }
        internal Joint (Point pt,Loads load)
        {

            Pt = pt;
            X = pt.X;
            Y = pt.Y;
            Z = pt.Z;
            loads = load;
            ID = JointCounter.GetNextUnusedID();
            JointCounter.RegJointForID(ID, this);

        }
       
    }
}
