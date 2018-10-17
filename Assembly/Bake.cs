using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoETABS.Structure;
using DynamoETABS.Definitions;
using ETABS2016;
using Autodesk.DesignScript.Geometry;
namespace DynamoETABS.Assembly
{
    public class Bake
    {
        internal Beam Beams { get; set; }
        internal Frame_Section beamSections { get; set; }


        public static bool ToEABS(Column Columns, Beam Beams, Slab Slabs, Frame_Section FrameSections, Slab_Section SlabSections)
        {
            long r;

            ETABS2016.cOAPI etabs = (ETABS2016.cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            ETABS2016.cSapModel model = etabs.SapModel;
            string Nm = null;

            double[] Stiff = new double[8];
            Stiff[0] = FrameSections.FSM.A;
            Stiff[1] = FrameSections.FSM.V2;
            Stiff[2] = FrameSections.FSM.V3;
            Stiff[3] = FrameSections.FSM.T;
            Stiff[4] = FrameSections.FSM.M2;
            Stiff[5] = FrameSections.FSM.M3;
            Stiff[6] = 1.0; Stiff[7] = 1.0;




            r = model.PropFrame.SetRectangle(FrameSections.SecName, FrameSections.SecMat, FrameSections.BD, FrameSections.BW);
            r = model.PropFrame.SetRebarBeam(FrameSections.SecName, "A615Gr60", "A615Gr60", 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
            r = model.PropFrame.SetModifiers(FrameSections.SecName, ref Stiff);

            r = model.PropArea.SetSlab(SlabSections.SecName,eSlabType.Slab,eShellType.ShellThin, SlabSections.SecMat, SlabSections.H);


            r = model.FrameObj.AddByCoord(Beams.BaseCrv.StartPoint.X,
                                            Beams.BaseCrv.StartPoint.Y,
                                            Beams.BaseCrv.StartPoint.Z,
                                            Beams.BaseCrv.EndPoint.X,
                                            Beams.BaseCrv.EndPoint.Y,
                                            Beams.BaseCrv.EndPoint.Z, ref Nm,
                                            Beams.BeamSec.SecName

                );
            r = model.FrameObj.AddByCoord(Columns.BaseCrv.StartPoint.X,
                                            Columns.BaseCrv.StartPoint.Y,
                                            Columns.BaseCrv.StartPoint.Z,
                                            Columns.BaseCrv.EndPoint.X,
                                            Columns.BaseCrv.EndPoint.Y,
                                            Columns.BaseCrv.EndPoint.Z, ref Nm,
                                            Columns.ColumnSec.SecName

                );

            Curve[] PMCurves = Slabs.BaseSurf.PerimeterCurves();
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
            r = model.AreaObj.AddByPoint(ProfilePts.Count(), ref names, ref dummyarea);
            return true;

        }


    }
}
