using System;
using System.Data;
using System.IO;
namespace HelperLib
{
    public class Helper
    {
        public DataTable InitializeDataTable()
        {
            DataTable dt = new DataTable("DriverTrips");
            dt.Columns.Add("DriverName", typeof(string));
            dt.Columns.Add("Distance", typeof(double));
            dt.Columns.Add("Speed", typeof(double));
            return dt;
        }

        public string[] LoadInputFile(string inputFileLocation)
        {
            if (File.Exists(inputFileLocation))
                return File.ReadAllLines(inputFileLocation);
            else
            {
                Console.WriteLine("Input file not found, please review location: " + inputFileLocation);
                return new string[0];
            }

        }

        public void SortAndPrintResults(DataTable drivertrips)
        {
            drivertrips.DefaultView.Sort = "Distance DESC";
            drivertrips = drivertrips.DefaultView.ToTable();

            //Print the results
            foreach (DataRow row in drivertrips.Rows)
            {
                int distance = 0, speed = 0;
                distance = Convert.ToInt32(row["Distance"]);
                speed = Convert.ToInt32(row["Speed"]);

                string log = row["DriverName"] + ": " + distance + " miles";
                if (distance > 0)
                    log += " @ " + speed + " mph";
                Console.WriteLine(log);
                Console.WriteLine();
            }
        }
    }
}
