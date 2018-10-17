using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Autodesk.DesignScript.Geometry;
using DynamoETABS.Definitions;
using Autodesk.DesignScript.Runtime;
using DynamoServices;

namespace DynamoETABS.Structure
{
    public class Beam : Element
    {


        //Base line
        internal Line BaseCrv { get; set; }
        //Section Property
        internal Frame_Section BeamSec { get; set; }
        //Releases
        //internal Releases Releases { get; set; }
        ////Loads
        //internal List<Loads> Loads { get; set; }
        //ID
        internal int ID { get; set; }
        //Label
        internal string Label { get; set; }


        [RegisterForTrace]
        public static Beam ByLine(Line Line,Frame_Section BeamSection)
        {
            Beam beam;
            BeamID beamID = null;

            Dictionary<string, ISerializable> getBeams = ProtoCore.Lang.TraceUtils.GetObjectFromTLS();
            foreach (var k in getBeams.Keys)
            {
                beamID = getBeams[k] as BeamID;
            }

            if (beamID == null)
            {
                beam = new Beam(Line, BeamSection);
                beam.Label = string.Format("Beam_{0}", beam.ID.ToString());

            }
            else
            {
                beam = BeamCounter.GetBeamByID(beamID.IntID);

                beam.BaseCrv = Line;
                beam.BeamSec = BeamSection;

            }



            Dictionary<string, ISerializable> beams = new Dictionary<string, ISerializable>();
            beams.Add(TRACE_ID, new BeamID { IntID = beam.ID });
            ProtoCore.Lang.TraceUtils.SetObjectToTLS(beams);


            return beam;
        }


        internal Beam() { }
        internal Beam(Line baseCrv, Frame_Section beamSec)
        {
            BaseCrv = baseCrv;
            BeamSec = beamSec;
            ID = BeamCounter.GetNextUnusedID();
            BeamCounter.RegBeamForID(ID, this);

        }

    }
}
