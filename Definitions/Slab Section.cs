﻿using System;
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
        //Shell Type
        internal eShellType ShellType { get; set; }
        

        //Define a new Slab_Section object 
        public static Slab_Section DefineSlabSection(string SectionName, string MaterialProperty, double Thickness, Shell_Stiffness_Modifier Stiffness_Modifier, eSlabType SlabType, eShellType ShellType  )
        {

            Slab_Section SlabSec = new Slab_Section(SectionName, MaterialProperty, Thickness, Stiffness_Modifier, SlabType,ShellType);

            return SlabSec;

        }

        //Constructors
        internal Slab_Section() { }

        internal Slab_Section(string secName, string secMat, double Th, Shell_Stiffness_Modifier sSM, eSlabType slabType , eShellType shellType )
        {
            SecName = secName;
            SecMat = secMat;
            H = Th;
            SSM = sSM;
            SlabType = slabType;
            ShellType = shellType;
        }

    }
}
