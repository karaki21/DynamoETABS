using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Autodesk.DesignScript.Geometry;
using DynamoETABS.Definitions;
using Autodesk.DesignScript.Runtime;
using DynamoServices;


namespace DynamoETABS.Structure
{
    [SupressImportIntoVM]
    public class Element
    {

        public Type Type { get; set; }

        internal const string TRACE_ID = "{0459D869-0C72-447F-96D8-08A7FB92214B}-REVIT";
    }

    [SupressImportIntoVM]
    public enum Type
    {
        Beam,
        Column,
        Slab,
        Wall,
        Joint
    }
}
