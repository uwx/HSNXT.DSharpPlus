using System;
using System.Data;
using HSNXT.ComLib.Application;
using HSNXT.ComLib.CsvParse;
//<doc:using>
//</doc:using>


namespace HSNXT.ComLib.Samples
{
    /// <summary>
    /// Example for the CsvParse namespace.
    /// </summary>
    public class Example_Csv : App
    {

        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
			//<doc:example>
            // See CommonLibrary.UnitTests Source code for actual csv files.
            var text = GetSampleCsv();
            var csv = Csv.LoadText(text, true);

            // 1. Get cell at row 0, column 1
            var cell0 = csv.Get<string>(0, 1);

            // 2. Get cell at row 0, column called "FilePath"
            var cell2 = csv.Get<string>(0, "FilePath");

            // 3. Number of columns
            var colCount = csv.Columns.Count;

            // 4. Number of rows
            var rowCount = csv.Data.Count;

            // 5. Column name at index 2
            var col2 = csv.Columns[1];

            // 6. Get int id at row 2
            var id = csv.Get<int>(2, 0);

            // 7. Iterate over all the cells in column named "Date" starting at row 0.
            csv.ForEach<DateTime>("Date", 0, (row, col, val) => 
            {
                Console.WriteLine("Row[{0}]Col[{1}] : {2}", row, col, val.ToString()); 
            });

            // 8. Get the csv data as a datatable.
            var table = csv.ToDataTable("My_Sample_Data");

            // 9. Iterate over rows / columns
            for(var row = 0; row < csv.Data.Count; row++)
            {
                for (var col = 0; col < csv.Columns.Count; col++)
                {
                    var cellVal = csv.Data[row][col] as string;
                }
            }
			//</doc:example>
            return BoolMessageItem.True;
        }


        private string GetSampleCsv()
        {
            var csv = @"Id,NonAlphaNumeric,FilePath,Date" + Environment.NewLine
                    + @"1,(`~!@#$%^&*()_+-=[]\{}|<>?./;:),C:\pictures\100_01.JPG,4/10/2009 11:27 AM" + Environment.NewLine
                    + @"2,(`~!@#$%^&*()_+-=[]\{}|<>?./;:),C:\pictures\100_01.JPG,4/11/2009 11:37 AM" + Environment.NewLine
                    + @"3,(`~!@#$%^&*()_+-=[]\{}|<>?./;:),C:\pictures\100_01.JPG,4/12/2009 11:47 AM";
            return csv;
        }


        private void WriteCsv()
        {
            // Write out the csv doc to a file.
            // doc.Write(@"C:\temp\test.csv", ",");
            /*
             * string text = "'Id', 'Name',   'Desc'" + Environment.NewLine
                        + "'0',  'Art',    'Art class'" + Environment.NewLine
                        + "'1',  'Sports', 'all sports'";
            var cols = new List<string>() { "Id", "Name", "Desc" };
            var data = new List<List<string>>(){ new List<string>() { "0",  "Baseball", "MLB"},
                                                          new List<string>() { "1",  "Football", "NFL"} };

            Csv.Write("StockData.csv", data, ";", cols);
            */
        }
    }
}
