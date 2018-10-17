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
    public class Column : Element
    {

        //Base line
        internal Line BaseCrv { get; set; }
        //Section Property
        internal Frame_Section ColumnSec { get; set; }
        //Orientation
        internal int Orientation { get; set; }

        [RegisterForTrace]
        public static Column ByLine(Line line, Frame_Section ColumnSection, int ColumnOrient = 0)
        {

            Column NColumn = new Column();
            NColumn.BaseCrv = line;
            NColumn.ColumnSec = ColumnSection;
            NColumn.Orientation = ColumnOrient;

            return NColumn;
        }


        internal Column() { }
        internal Column(Line baseCrv, Frame_Section ColumnSection, int ColumnOrient)
        {
            BaseCrv = baseCrv;
            ColumnSec = ColumnSection;
            Orientation = ColumnOrient;

        }

    }
}
