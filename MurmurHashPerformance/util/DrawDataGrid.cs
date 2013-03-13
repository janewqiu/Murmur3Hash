using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace MurmurHashPerformance.util
{
    public class DrawDataGrid
    {
        public DrawDataGrid(string tableTitle, params object[] headers)
        {
            Init(tableTitle, headers);
        }

        public DrawDataGrid()
        {
        }

        public DrawDataGrid( params object[] headers)
        {
            Init(string.Empty, headers);  
        }

        private void Init(string tableTitle, params object[] headers)
        {
            tableHeader = tableTitle;
            debugTable = new DataTable();
            foreach (var header in headers)
            {
                debugTable.Columns.Add(header.ToString(), typeof(string));
            }     
        }

        private string tableHeader = string.Empty;
        private DataTable debugTable = null;
        private bool HasHeader = true;
        public void AddDebugRow(params object[] values)
        {
            if (debugTable == null)
            {
                HasHeader = false;
                this.Init(string.Empty, values);          
            }
            else
                debugTable.Rows.Add(values);
        }

        public  string RenderToText(DataTable dataTable)
        {

            StringBuilder output = new StringBuilder();

            var columnsWidths = new int[dataTable.Columns.Count];

            // Get column widths
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var length = row[i].ToString().Length;
                    if (columnsWidths[i] < length)
                        columnsWidths[i] = length;
                }
            }

            // Get Column Titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var length = dataTable.Columns[i].ColumnName.Length;
                if (columnsWidths[i] < length)
                    columnsWidths[i] = length;
            }

            int tablewidth = columnsWidths.Sum()+ columnsWidths.Length*3 +1;
            // Write Column titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var text = dataTable.Columns[i].ColumnName;
                output.Append("|" + PadCenter(text, columnsWidths[i] + 2));              
            }
            output.Append("|\n" + new string('=', tablewidth) + "\n");

            // Write Rows
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var text = row[i].ToString();
                    output.Append("|" + PadCenter(text, columnsWidths[i] + 2));
                }
                output.Append("|\n");
            }

            return output.ToString();
        }


        private  string PadCenter(string text, int maxLength)
        {
            int diff = maxLength - text.Length;
            return new string(' ', diff / 2) + text + new string(' ', (int)(diff / 2.0 + 0.5));

        } 
    }
}
