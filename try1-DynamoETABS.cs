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

    public static class DynamoETABS
    {

        

        public static Dictionary<int, int> intdic = new Dictionary<int, int>();

        
        //     internal class SectionProp
        //    {

        //    }

        //    internal class Frame
        //   {
        //      internal Line line { get; set; }

        //     internal SectionProp sectionprop { get; set; }

        public static bool StartETABS(bool Run)
        {

            
            
            if (Run == true)
            {
                ETABS2016.cHelper hlp = new ETABS2016.Helper();
                ETABS2016.cOAPI etabs = hlp.CreateObject("C:\\Program Files\\Computers and Structures\\ETABS 2016\\ETABS.exe");

                ETABS2016.cSapModel model = etabs.SapModel;
                long r;
                r = etabs.ApplicationStart();

                r = model.InitializeNewModel(eUnits.kN_m_C);
                r = model.File.NewBlank();
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool DeletePrev(bool TF)
        {
            ETABS2016.cOAPI etabs = (ETABS2016.cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            ETABS2016.cSapModel model = etabs.SapModel;
            long r;

            r = model.SelectObj.All();
            r = model.FrameObj.Delete("1", eItemType.SelectedObjects);
            return true;
        }

        public static bool Frame_from_line(Line line, bool OK)
        {
            ETABS2016.cOAPI etabs = (ETABS2016.cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            ETABS2016.cSapModel model = etabs.SapModel;
            long r;
            Point i = line.StartPoint;
            Point j = line.EndPoint;

            double xi = i.X;
            double yi = i.Y;
            double zi = i.Z;

            double xj = j.X;
            double yj = j.Y;
            double zj = j.Z;

            string Name = "1";
            r = model.FrameObj.AddByCoord(xi, yi, zi, xj, yj, zj, ref Name);

            return true;

        }

        public static bool SaveModel(bool Save,string FilePath, int Trial )
        {
            ETABS2016.cOAPI etabs = (ETABS2016.cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            ETABS2016.cSapModel model = etabs.SapModel;
            long r;
            string path = FilePath + "\\Trial " + Trial;
            if (Save==true)
            {
                r = model.File.Save(path);
                r = etabs.ApplicationExit(false);
                return true;
            }
            else
            {
                return false;
            }
        }
      
    }
}
