using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoETABS.Definitions;
using ETABS2016;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DynamoServices;

namespace DynamoETABS.Definitions
{
    public class Slab_Section
    {
        //Section Name
        internal string SecName { get; set; }
        //Section Material
        internal string SecMat { get; set; }
        //Section Thickness
        internal double H { get; set; }
        //Property Modifier
        internal Shell_Stiffness_Modifier SSM { get; set; }
        //Modelling Type
        internal eSlabType SlabType { get; set; }
        


        public static Slab_Section SlabSection(string SectionName, string MaterialProperty, double Thickness, Shell_Stiffness_Modifier Stiffness_Modifier, eSlabType SlabType  )
        {

            Slab_Section SlabSec = new Slab_Section(SectionName, MaterialProperty, Thickness, Stiffness_Modifier, SlabType);

            return SlabSec;

        }

        internal Slab_Section() { }

        internal Slab_Section(string secName, string secMat, double Th, Shell_Stiffness_Modifier sSM, eSlabType slabType = eSlabType.Slab )
        {
            SecName = secName;
            SecMat = secMat;
            H = Th;
            SSM = sSM;
            SlabType = slabType;
            
        }

    }
}
