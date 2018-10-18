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

        public static bool ToETABS(List<Column> Columns, List<Beam> Beams,List<Wall> Walls, List<Slab> Slabs,  List<Frame_Section> FrameSections,List<Wall_Section> WallSections, List<Slab_Section> SlabSections)
        {
            
            long r;
            //Link to ETABS
            ETABS2016.cOAPI etabs = (ETABS2016.cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            ETABS2016.cSapModel model = etabs.SapModel;

            //Delete Current Model
            string Nm = null;
            r = model.SelectObj.All();
            r = model.FrameObj.Delete("",eItemType.SelectedObjects);
            r = model.SelectObj.All();
            r = model.AreaObj.Delete("", eItemType.SelectedObjects);
            
            //Define Frame Sections
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

            //Define Wall Sections
            foreach (var WS in WallSections)
            {
                r = model.PropArea.SetWall(WS.SecName, eWallPropType.Specified, WS.ShellType, WS.SecMat, WS.H);
            }

            //Define Slab sections
            foreach (var SS in SlabSections)
            {
                r = model.PropArea.SetSlab(SS.SecName, SS.SlabType, SS.ShellType, SS.SecMat, SS.H);
            }

            //Draw Beam Elements
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

                if (bm.Release!= null)
                {
                    double[] v = { 0, 0, 0, 0, 0, 0 };
                    bool[] ii = bm.Release.II;
                    bool[] jj = bm.Release.JJ;

                    r = model.FrameObj.SetReleases(Nm,ref ii,ref jj,ref v,ref v);
                }
                if (bm.Load!=null)
                {
                    int i = 0;
                    foreach (string lc in bm.Load.LoadCase)
                    {
                        r = model.FrameObj.SetLoadDistributed(Nm, lc, 1, 10, 0, 1, bm.Load.LoadValue[i], bm.Load.LoadValue[i]);
                        i++;
                    }
                }
                r = model.FrameObj.ChangeName(Nm, bm.Label);

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
                r = model.FrameObj.ChangeName(Nm, col.Label);
            }

            foreach (var wall in Walls)
            {
                Curve[] PMCurves = wall.BaseSurf.PerimeterCurves();
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
                
                r = model.AreaObj.AddByPoint(ProfilePts.Count(), ref names, ref Nm, wall.WallSec.SecName);

                r = model.AreaObj.ChangeName(Nm, wall.Label);

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
                
                r = model.AreaObj.AddByPoint(ProfilePts.Count(), ref names, ref Nm,slab.SlabSec.SecName);

                if (slab.Load != null)
                {
                    int i = 0;
                    foreach (string lc in slab.Load.LoadCase)
                    {
                        r = model.AreaObj.SetLoadUniform(Nm, lc, slab.Load.LoadValue[i], 10);
                        i++;
                    }
                }


                r = model.AreaObj.ChangeName(Nm, slab.Label);

            }
            /*
            string[] stnames = new string[StoryData.StoryNames.Count()];
            double[] stelev = new double[StoryData.StoryNames.Count()];
            double[] stheights = new double[0];
            bool[] ismaster = new bool[StoryData.StoryNames.Count()];
            string[] similarstory = new string[StoryData.StoryNames.Count()];
            double[] splen = new double[StoryData.StoryNames.Count()];

            stelev[0] = StoryData.StoryElevations[0];

            for (int i = 0; i <= ismaster.Length; i++)
            {
                stnames[i] = StoryData.StoryNames[i];
                stelev[i+1] = StoryData.StoryElevations[i+1];
                ismaster[i] = false;
                similarstory[i] = string.Empty;
                splen[i] = 0.0;

            }
            

            r = model.Story.SetStories(stnames, stelev, stheights, ismaster,similarstory,ismaster,splen);
            */
            Beams.Clear();
            Columns.Clear();
            Slabs.Clear();
            FrameSections.Clear();
            WallSections.Clear();
            SlabSections.Clear();
            return true;
        }

        internal Bake() { }

    }
}
