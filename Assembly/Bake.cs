using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoETABS.Structure;
using DynamoETABS.Definitions;
using ETABS2016;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
namespace DynamoETABS.Assembly
{
    
    public class Bake
    {


        /// <summary>
        ///  Create or Update SAP2000 model from Dynamo Structural Model
        /// </summary>
        /// <returns></returns>
        public static bool ToETABS(List<Column> Columns, List<Beam> Beams, List<Slab> Slabs, List<Frame_Section> FrameSections, List<Slab_Section> SlabSections)
        {
            

            long r;

            ETABS2016.cOAPI etabs = (ETABS2016.cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            ETABS2016.cSapModel model = etabs.SapModel;
            string Nm = null;

            foreach (var FS in FrameSections)
            {
                double[] Stiff = new double[8];
                Stiff[0] = FS.FSM.A;
                Stiff[1] = FS.FSM.V2;
                Stiff[2] = FS.FSM.V3;
                Stiff[3] = FS.FSM.T;
                Stiff[4] = FS.FSM.M2;
                Stiff[5] = FS.FSM.M3;
                Stiff[6] = 1.0; Stiff[7] = 1.0;

                r = model.PropFrame.SetRectangle(FS.SecName, FS.SecMat, FS.BD, FS.BW);
                if (FS.IsBeam)
                {
                    r = model.PropFrame.SetRebarBeam(FS.SecName, "A615Gr60", "A615Gr60", 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
                }
                r = model.PropFrame.SetModifiers(FS.SecName, ref Stiff);
            }


            foreach (var SS in SlabSections)
            {
                r = model.PropArea.SetSlab(SS.SecName, eSlabType.Slab, eShellType.ShellThin, SS.SecMat, SS.H);
            }

            foreach (var bm in Beams)
            {
                r = model.FrameObj.AddByCoord(bm.BaseCrv.StartPoint.X,
                                                bm.BaseCrv.StartPoint.Y,
                                                bm.BaseCrv.StartPoint.Z,
                                                bm.BaseCrv.EndPoint.X,
                                                bm.BaseCrv.EndPoint.Y,
                                                bm.BaseCrv.EndPoint.Z, ref Nm,
                                                bm.BeamSec.SecName

                    );
            }

            foreach (var col in Columns)
            {
                r = model.FrameObj.AddByCoord(col.BaseCrv.StartPoint.X,
                                                col.BaseCrv.StartPoint.Y,
                                                col.BaseCrv.StartPoint.Z,
                                                col.BaseCrv.EndPoint.X,
                                                col.BaseCrv.EndPoint.Y,
                                                col.BaseCrv.EndPoint.Z, ref Nm,
                                                col.ColumnSec.SecName

                    );
            }

            foreach (var slab in Slabs)
            {
                Curve[] PMCurves = slab.BaseSurf.PerimeterCurves();
                List<Point> SurfacePts = new List<Point>();
                foreach (var crv in PMCurves)
                {
                    SurfacePts.Add(crv.StartPoint);
                }
                List<string> ProfilePts = new List<string>();
                foreach (var v in SurfacePts)
                {
                    string dummy = null;
                    r = model.PointObj.AddCartesian(v.X, v.Y, v.Z, ref dummy);
                    ProfilePts.Add(dummy);
                }

                string[] names = ProfilePts.ToArray();
                string dummyarea = string.Empty;
                r = model.AreaObj.AddByPoint(ProfilePts.Count(), ref names, ref dummyarea,slab.SlabSec.SecName);
                
            }


            Beams.Clear();
            Columns.Clear();
            Slabs.Clear();
            FrameSections.Clear();
            SlabSections.Clear();
            return true;
        }


    }
}
