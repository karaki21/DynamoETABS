using Autodesk.DesignScript.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoETABS.Definitions;
using Autodesk.DesignScript.Runtime;
using System.Runtime.Serialization;
using DynamoServices;


namespace DynamoETABS.Structure
{
    public class Slab : Element
    {


        //Surface
        internal Surface BaseSurf { get; set; }
        //Section
        internal Slab_Section SlabSec { get; set; }
        //Loads
        internal Loads Load { get; set; }
        //ID
        internal int ID { get; set; }
        //Label
        internal string Label { get; set; }

        //New Slab by dynamo surface
        [RegisterForTrace]
        public static Slab BySurface(Surface Surface, Slab_Section SlabSection)
        {

            Slab slab;
            SlabID slabID = null;

            //Check if slab already exists
            Dictionary<string, ISerializable> getSlabs = ProtoCore.Lang.TraceUtils.GetObjectFromTLS();
            foreach (var k in getSlabs.Keys)
            {
                slabID = getSlabs[k] as SlabID;
            }

            if (slabID == null)
            {
                //If the slab doesnt exist, create new slab and assign ID
                slab = new Slab(Surface, SlabSection);
                slab.Label = string.Format("Slab_{0}", slab.ID.ToString());

            }
            else
            {
                //If the slab exists, update attributes of existing slab element
                slab = SlabCounter.GetSlabByID(slabID.IntID);

                slab.BaseSurf = Surface;
                slab.SlabSec = SlabSection;


            }


            //Add slab element to TLS
            Dictionary<string, ISerializable> slabs = new Dictionary<string, ISerializable>();
            slabs.Add(TRACE_ID, new SlabID { IntID = slab.ID });
            ProtoCore.Lang.TraceUtils.SetObjectToTLS(slabs);


            return slab;

        }

        //Add loads to slab object
        public static Slab AddLoadsToSlab (ref Slab Slab, Loads Load)
        {
            Slab.Load = Load;
            return Slab;
        }



        //Constructors
        internal Slab() { }
        internal Slab(Surface surface, Slab_Section slabSection)
        {
            BaseSurf = surface;
            SlabSec = slabSection;
            ID = SlabCounter.GetNextUnusedID();
            SlabCounter.RegSlabForID(ID, this);
        }
    }
}
