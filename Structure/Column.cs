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
    public class Column : Element
    {

        //Base line
        internal Line BaseCrv { get; set; }
        //Section Property
        internal Frame_Section ColumnSec { get; set; }
        //Orientation
        internal int Orientation { get; set; }
        //ID
        internal int ID { get; set; }
        //Label
        internal string Label { get; set; }


        [RegisterForTrace]
        public static Column ByLine(Line Line, Frame_Section ColumnSection, int ColumnOrient = 0)
        {
            Column column;
            ColumnID columnID = null;

            //Check if column already exists
            Dictionary<string, ISerializable> getColumns = ProtoCore.Lang.TraceUtils.GetObjectFromTLS();
            foreach (var k in getColumns.Keys)
            {
                columnID = getColumns[k] as ColumnID;
            }

            if (columnID == null)
            {
                //If the column doesnt exist, create new column and assign ID
                column = new Column(Line, ColumnSection, ColumnOrient);
                column.Label = string.Format("Column_{0}", column.ID.ToString());

            }
            else
            {
                //If the column exists, update attributes of existing column element
                column = ColumnCounter.GetColumnByID(columnID.IntID);

                column.BaseCrv = Line;
                column.ColumnSec = ColumnSection;
                column.Orientation = ColumnOrient;

            }


            //Add column element to TLS
            Dictionary<string, ISerializable> columns = new Dictionary<string, ISerializable>();
            columns.Add(TRACE_ID, new BeamID { IntID = column.ID });
            ProtoCore.Lang.TraceUtils.SetObjectToTLS(columns);


            return column;
            
        }

        //Constructors
        internal Column() { }
        internal Column(Line baseCrv, Frame_Section ColumnSection, int ColumnOrient)
        {
            BaseCrv = baseCrv;
            ColumnSec = ColumnSection;
            Orientation = ColumnOrient;
            ID = ColumnCounter.GetNextUnusedID();
            ColumnCounter.RegColumnForID(ID, this);

        }

    }
}
