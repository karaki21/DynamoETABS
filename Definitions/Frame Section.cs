using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoETABS.Definitions;

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DynamoServices;

namespace DynamoETABS.Definitions
{
    public class Frame_Section
    {
        //Section Name
        internal string SecName { get; set; }
        //Section Material
        internal string SecMat { get; set; }
        //Section Width
        internal double BW { get; set; }
        //Section Depth 
        internal double BD { get; set; }
        //Property Modifier
        internal Frame_Stiffness_Modifier FSM { get; set; }
        //Import Database for steel sections
        internal string DB { get; set; }
        //Column or Beam Check
        internal Boolean IsBeam { get; set; }


        public static Frame_Section RectangularFrameSection(string SectionName, string MaterialProperty, double Width, double Depth, Frame_Stiffness_Modifier Stiffness_Modifier, Boolean IsBeam = false)
        {

            Frame_Section FSec = new Frame_Section(SectionName, MaterialProperty, Width, Depth, Stiffness_Modifier, IsBeam);

            return FSec;

        }

        public static Frame_Section ImportSteelSection(string SectionName, string MaterialProperty, string Section_Database, Boolean IsBeam)
        {

            Frame_Section FSec = new Frame_Section(SectionName, MaterialProperty, Section_Database, IsBeam);

            return FSec;

        }


        internal Frame_Section() { }

        internal Frame_Section(string secName, string secMat, double bW, double bD, Frame_Stiffness_Modifier fSM, Boolean isBeam)
        {
            SecName = secName;
            SecMat = secMat;
            BW = bW;
            BD = bD;
            FSM = fSM;
            IsBeam = isBeam;
        }

        internal Frame_Section(string secName, string secMat, string dB, Boolean isBeam)
        {
            SecName = secName;
            SecMat = secMat;
            DB = dB;
            IsBeam = isBeam;
        }
    }
}
