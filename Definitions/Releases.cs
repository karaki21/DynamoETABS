using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoETABS.Definitions
{
    public class Releases
    {
        internal bool[] II { get; set; }
        internal bool[] JJ { get; set; }

        public static Releases NewRelease(Boolean StartU1 = false,Boolean StartU2 = false, Boolean StartU3 = false, Boolean StartR1 = false, Boolean StartR2 = false, Boolean StartR3 = false, Boolean EndU1 = false, Boolean EndU2 = false, Boolean EndU3 = false, Boolean EndR1 = false, Boolean EndR2 = false, Boolean EndR3 = false)
        {
            Releases Rel= new Releases();

            Rel.II[0] = StartU1;
            Rel.II[1] = StartU2;
            Rel.II[2] = StartU3;
            Rel.II[3] = StartR1;
            Rel.II[4] = StartR2;
            Rel.II[5] = StartR3;
            Rel.II[0] = EndU1;
            Rel.II[1] = EndU2;
            Rel.II[2] = EndU3;
            Rel.II[3] = EndR1;
            Rel.II[4] = EndR2;
            Rel.II[5] = EndR3;


            return Rel;
        }


        internal Releases() { }

    }
}
