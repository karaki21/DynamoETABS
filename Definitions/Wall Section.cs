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
    public class Wall_Section
    {
        //Section Name
        internal string SecName { get; set; }
        //Section Material
        internal string SecMat { get; set; }
        //Section Thickness
        internal double H { get; set; }
        //Property Modifier
        internal Shell_Stiffness_Modifier SSM { get; set; }
        //Shell Type
        internal eShellType ShellType { get; set; }


        //Define a new Wall_Section object 
        public static Wall_Section DefineWallSection(string SectionName, string MaterialProperty, double Thickness, Shell_Stiffness_Modifier Stiffness_Modifier, eShellType ShellType)
        {

            Wall_Section WallSec = new Wall_Section(SectionName, MaterialProperty, Thickness, Stiffness_Modifier, ShellType);

            return WallSec;

        }

        //Constructors
        internal Wall_Section() { }

        internal Wall_Section(string secName, string secMat, double Th, Shell_Stiffness_Modifier sSM, eShellType shellType)
        
        {
            SecName = secName;
            SecMat = secMat;
            H = Th;
            SSM = sSM;
            ShellType = shellType;
        }

    }
}

