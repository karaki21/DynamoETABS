using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoETABS.Definitions
{
    public class Loads
    {
        //Load Case Name List
        internal List<string> LoadCase { get; set; }
        //Loading Value List }
        internal List<double> LoadValue { get; set; }
        //Loading Type
        internal String LoadType { get; set; }
        
        //Creates Load Object
        public static Loads CreateLoads(List<string> Loadcase, List<double> Loadvalue, String Loadtype = "Line")
        {

            Loads Load = new Loads(Loadcase,Loadvalue,Loadtype);

            return Load;

        }

        //Load Object Constructor
        internal Loads() { }
        internal Loads(List<string> loadcase, List<double> loadvalue, string loadtype)
        {
            LoadCase = loadcase;
            LoadValue = loadvalue;
            LoadType = loadtype;
        }


    }
}
