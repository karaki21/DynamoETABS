using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoETABS.Definitions
{
    public class Releases
    {
        internal List<Boolean> II { get; set; }
        internal List<Boolean> JJ { get; set; }

        public static Releases NewRelease(Boolean StartU1 = false,Boolean StartU2 = false, Boolean StartU3 = false, Boolean StartR1 = false, Boolean StartR2 = false, Boolean StartR3 = false, Boolean EndU1 = false, Boolean EndU2 = false, Boolean EndU3 = false, Boolean EndR1 = false, Boolean EndR2 = false, Boolean EndR3 = false)
        {
            Releases Rel= new Releases();

            Rel.II.Add(StartU1);
            Rel.II.Add(StartU2);
            Rel.II.Add(StartU3);
            Rel.II.Add(StartR1);
            Rel.II.Add(StartR2);
            Rel.II.Add(StartR3);
            Rel.II.Add(EndU1);
            Rel.II.Add(EndU2);
            Rel.II.Add(EndU3);
            Rel.II.Add(EndR1);
            Rel.II.Add(EndR2);
            Rel.II.Add(EndR3);


            return Rel;
        }


        internal Releases() { }

    }
}
