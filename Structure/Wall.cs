using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DynamoETABS.Definitions;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DynamoServices;
using System.Runtime.Serialization;


namespace DynamoETABS.Structure
{
    public class Wall : Element
    {

        //Surface
        internal Surface BaseSurf { get; set; }
        //Section
        internal Wall_Section WallSec { get; set; }
        //Loads
        internal Loads Load { get; set; }
        //ID
        internal int ID { get; set; }
        //Label
        internal string Label { get; set; }

        //New Wall by dynamo surface
        [RegisterForTrace]
        public static Wall BySurface(Surface Surface, Wall_Section WallSection)
        {

            Wall wall;
            WallID wallID = null;

            //Check if wall already exists
            Dictionary<string, ISerializable> getWalls = ProtoCore.Lang.TraceUtils.GetObjectFromTLS();
            foreach (var k in getWalls.Keys)
            {
                wallID = getWalls[k] as WallID;
            }

            if (wallID == null)
            {
                //If the wall doesnt exist, create new wall and assign ID
                wall = new Wall(Surface, WallSection);
                wall.Label = string.Format("Wall_{0}", wall.ID.ToString());

            }
            else
            {
                //If the wall exists, update attributes of existing wall element
                wall = WallCounter.GetWallByID(wallID.IntID);

                wall.BaseSurf = Surface;
                wall.WallSec = WallSection;


            }


            //Add wall element to TLS
            Dictionary<string, ISerializable> walls = new Dictionary<string, ISerializable>();
            walls.Add(TRACE_ID, new WallID { IntID = wall.ID });
            ProtoCore.Lang.TraceUtils.SetObjectToTLS(walls);


            return wall;

        }

        

        //Constructors
        internal Wall() { }
        internal Wall(Surface surface, Wall_Section wallSection)
        {
            BaseSurf = surface;
            WallSec = wallSection;
            ID = WallCounter.GetNextUnusedID();
            WallCounter.RegWallForID(ID, this);
        }
    }

}
}
