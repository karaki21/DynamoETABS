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



    /*
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
    */
}

