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
        //internal List<Loads> Loads { get; set; }
        ////ID
        internal int ID { get; set; }
        //Label
        internal string Label { get; set; }

        [RegisterForTrace]
        public static Slab BySurface(Surface Surface, Slab_Section SlabSection)
        {
            Slab slab = new Slab(Surface, SlabSection);
            return slab;
        }

        public Slab(Surface surface, Slab_Section slabSection)
        {
            BaseSurf = surface;
            SlabSec = slabSection;
        }
    }
}
