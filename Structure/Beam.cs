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
        internal Releases Release { get; set; }
        //Loads
        internal Loads Load { get; set; }
        //ID
        internal int ID { get; set; }
        //Label
        internal string Label { get; set; }

        //Add beams by dynamo lines
        [RegisterForTrace]
        public static Beam ByLine(Line Line,Frame_Section BeamSection)
        {
            Beam beam;
            BeamID beamID = null;

            //Check if beam already exists
            Dictionary<string, ISerializable> getBeams = ProtoCore.Lang.TraceUtils.GetObjectFromTLS();
            foreach (var k in getBeams.Keys)
            {
                beamID = getBeams[k] as BeamID;
            }

            if (beamID == null)
            {
                //If the beam doesnt exist, create new beam and assign ID
                beam = new Beam(Line, BeamSection);
                beam.Label = string.Format("Beam_{0}", beam.ID.ToString());

            }
            else
            {
                //If the beam exists, update attributes of existing beam element
                beam = BeamCounter.GetBeamByID(beamID.IntID);

                beam.BaseCrv = Line;
                beam.BeamSec = BeamSection;

            }


            //Add beam element to TLS
            Dictionary<string, ISerializable> beams = new Dictionary<string, ISerializable>();
            beams.Add(TRACE_ID, new BeamID { IntID = beam.ID });
            ProtoCore.Lang.TraceUtils.SetObjectToTLS(beams);


            return beam;
        }

        //Add end releases to a beam
        public static Beam AddReleases (ref Beam Beams, Releases Releases)
        {
            Beams.Release = Releases;
            return Beams;
        }

        //Add loads attribute to beams
        public static Beam AddLoads(ref Beam Beams,Loads Loads)
        {

            Beams.Load = Loads;
            return Beams;

        }

        //Constructors 
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
