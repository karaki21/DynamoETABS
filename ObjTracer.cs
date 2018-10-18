using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
using System.Runtime.Serialization;


using DynamoETABS.Structure;


namespace DynamoETABS
{

    
    [SupressImportIntoVM]
    public static class JointCounter
    {
        public static int JointID = 0;

        public static int GetNextUnusedID()
        {
            int next = JointID;
            JointID++;
            return next;
        }

        public static Dictionary<int, Joint> JointDic = new Dictionary<int, Joint>();

        public static Joint GetJointByID(int id)
        {
            Joint joint;
            JointDic.TryGetValue(id, out joint);
            return joint;
        }

        public static void RegJointForID(int id, Joint joint)
        {
            if (JointDic.ContainsKey(id))
            {
                JointDic[id] = joint;
            }
            else
            {
                JointDic.Add(id, joint);
            }
        }
    }

    [SupressImportIntoVM]
    public class JointID : ISerializable
    {
        public int IntID { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("intID", IntID, typeof(int));
        }

        public JointID()
        {
            IntID = int.MinValue;
        }

        public JointID(SerializationInfo info, StreamingContext context)
        {
            IntID = (int)info.GetValue("intID", typeof(int));
        }
    }

    
   

    [SupressImportIntoVM]
    public static class BeamCounter
    {
        public static int BeamID = 0;

        public static int GetNextUnusedID()
        {
            int next = BeamID;
            BeamID++;
            return next;
        }

        public static Dictionary<int, Beam> BeamDic = new Dictionary<int, Beam>();

        public static Beam GetBeamByID(int id)
        {
            Beam beam;
            BeamDic.TryGetValue(id, out beam);
            return beam;
        }

        public static void RegBeamForID(int id, Beam beam)
        {
            if (BeamDic.ContainsKey(id))
            {
                BeamDic[id] = beam;
            }
            else
            {
                BeamDic.Add(id, beam);
            }
        }
    }

    [SupressImportIntoVM]
    public class BeamID : ISerializable
    {
        public int IntID { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("intID", IntID, typeof(int));
        }

        public BeamID()
        {
            IntID = int.MinValue;
        }

        public BeamID(SerializationInfo info, StreamingContext context)
        {
            IntID = (int)info.GetValue("intID", typeof(int));
        }
    }

    
    [SupressImportIntoVM]
    public static class ColumnCounter
    {
        public static int ColumnID = 0;

        public static int GetNextUnusedID()
        {
            int next = ColumnID;
            ColumnID++;
            return next;
        }

        public static Dictionary<int, Column> ColumnDic = new Dictionary<int, Column>();

        public static Column GetColumnByID(int id)
        {
            Column column;
            ColumnDic.TryGetValue(id, out column);
            return column;
        }

        public static void RegColumnForID(int id, Column column)
        {
            if (ColumnDic.ContainsKey(id))
            {
                ColumnDic[id] = column;
            }
            else
            {
                ColumnDic.Add(id, column);
            }
        }
    }

    [SupressImportIntoVM]
    public class ColumnID : ISerializable
    {
        public int IntID { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("intID", IntID, typeof(int));
        }

        public ColumnID()
        {
            IntID = int.MinValue;
        }

        public ColumnID(SerializationInfo info, StreamingContext context)
        {
            IntID = (int)info.GetValue("intID", typeof(int));
        }
    }
    
    



    [SupressImportIntoVM]
    public static class SlabCounter
    {
        public static int SlabID = 0;

        public static int GetNextUnusedID()
        {
            int next = SlabID;
            SlabID++;
            return next;
        }

        public static Dictionary<int, Slab> SlabDic = new Dictionary<int, Slab>();

        public static Slab GetSlabByID(int id)
        {
            Slab slab;
            SlabDic.TryGetValue(id, out slab);
            return slab;
        }

        public static void RegSlabForID(int id, Slab slab)
        {
            if (SlabDic.ContainsKey(id))
            {
                SlabDic[id] = slab;
            }
            else
            {
                SlabDic.Add(id, slab);
            }
        }
    }


    [SupressImportIntoVM]
    public class SlabID : ISerializable
    {
        public int IntID { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("intID", IntID, typeof(int));
        }

        public SlabID()
        {
            IntID = int.MinValue;
        }

        public SlabID(SerializationInfo info, StreamingContext context)
        {
            IntID = (int)info.GetValue("intID", typeof(int));
        }
    }

   
     
    [SupressImportIntoVM]
    public static class WallCounter
    {
        public static int WallID = 0;

        public static int GetNextUnusedID()
        {
            int next = WallID;
            WallID++;
            return next;
        }

        public static Dictionary<int, Wall> WallDic = new Dictionary<int, Wall>();

        public static Wall GetWallByID(int id)
        {
            Wall wall;
            WallDic.TryGetValue(id, out wall);
            return wall;
        }

        public static void RegWallForID(int id, Wall wall)
        {
            if (WallDic.ContainsKey(id))
            {
                WallDic[id] = wall;
            }
            else
            {
                WallDic.Add(id, wall);
            }
        }
    }


    [SupressImportIntoVM]
    public class WallID : ISerializable
    {
        public int IntID { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("intID", IntID, typeof(int));
        }

        public WallID()
        {
            IntID = int.MinValue;
        }

        public WallID(SerializationInfo info, StreamingContext context)
        {
            IntID = (int)info.GetValue("intID", typeof(int));
        }
    }

     

}

