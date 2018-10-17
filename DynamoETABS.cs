//System Libraries
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ETABS Library
using ETABS2016;

//Dynamo Libraries
using DynamoUnits;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DynamoServices;




namespace DynamoETABS
{
    public class Frame
    {

        internal Line line;

        internal string sectionprop;

        internal int rotation;

        internal string label;

        public static Frame From_Line(Line line, string sectionprop, int rotation)
        {
            ETABS2016.cOAPI etabs;
            ETABS2016.cHelper hlp = new ETABS2016.Helper();
            etabs = hlp.GetObject("CSI.ETABS.API.ETABSObject");
            long r;
            ETABS2016.cSapModel model = etabs.SapModel;


            Frame nfrm = new Frame();
            nfrm.line = line;
            nfrm.sectionprop = sectionprop;
            nfrm.rotation = rotation;

            Point i = nfrm.line.StartPoint;
            Point j = nfrm.line.EndPoint;

            double xi = i.X;
            double yi = i.Y;
            double zi = i.Z;

            double xj = j.X;
            double yj = j.Y;
            double zj = j.Z;

            string Name = "1";
            r = model.FrameObj.AddByCoord(xi, yi, zi, xj, yj, zj, ref Name);

            nfrm.label = Name;


            return nfrm;
        }

    }
  
    public static class DynamoETABS
    {
   //     internal class SectionProp
    //    {

    //    }

    //    internal class Frame
     //   {
      //      internal Line line { get; set; }

       //     internal SectionProp sectionprop { get; set; }

            

      //  }

            

            public static bool StartETABS(bool Run)
        {

            ETABS2016.cOAPI etabs;
            ETABS2016.cHelper hlp = new ETABS2016.Helper();
            etabs = NewMethod(hlp);
            long r;
            r = etabs.ApplicationStart();
            ETABS2016.cSapModel model = etabs.SapModel;
            r = model.InitializeNewModel(eUnits.kN_m_C);
            r = model.File.NewBlank();

            return true;
        }

        private static cOAPI NewMethod(cHelper hlp)
        {
            return hlp.CreateObject("C:\\Program Files\\Computers and Structures\\ETABS 2016\\ETABS.exe");
        }

    }
}
