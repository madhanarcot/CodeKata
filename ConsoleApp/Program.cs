using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
                Console.WriteLine("Usage: ConsoleApp C:/input.txt");
            else
            {
                HelperLib.Helper _helper = new HelperLib.Helper();
                DataTable drivertrips = _helper.InitializeDataTable();

                string[] lines = _helper.LoadInputFile(args[0]);

                //Load records into datatable
                foreach (string line in lines)
                {
                    if (line.ToLower().StartsWith("driver"))
                    {
                        string[] line_split_array = line.Split(" ", 2);
                        DataRow foundrow = drivertrips.Select("DriverName = '" + line_split_array[1] + "'").FirstOrDefault();

                        //Handle scenario where Trip record comes before Driver record 
                        if (foundrow == null)
                            drivertrips.Rows.Add(new object[] { line_split_array[1], 0, 0 });
                    }
                    else if (line.ToLower().StartsWith("trip"))
                    {
                        string drivername = string.Empty;
                        DateTime starttime = DateTime.MinValue;
                        DateTime endtime = DateTime.MinValue;
                        double distance = 0;
                        double speed = 0;

                        string[] line_split_array = line.Split(" ", 5);
                        drivername = line_split_array[1];
                        DateTime.TryParse(line_split_array[2], out starttime);
                        DateTime.TryParse(line_split_array[3], out endtime);
                        //If EndTime could not be parsed assume it is midnight 24:00, per readme doc
                        if (endtime == DateTime.MinValue)
                        {
                            //DateTime doesnt parse 24:00, parsing 00:00 and rolling it over to next day to achieve the same result
                            DateTime.TryParse("00:00", out endtime);
                            endtime = endtime.AddDays(1);
                        }
                        Double.TryParse(line_split_array[4], out distance);

                        //Ensure we dont divide by zero
                        if ((endtime - starttime).TotalMinutes > 0)
                        {
                            speed = (distance * 60) / (endtime - starttime).TotalMinutes;
                            //If Speed<5 or Speed>100 ignore the record
                            if (speed >= 5 && speed <= 100)
                            {
                                //If Driver found with Distance/Speed=0, update Distance & Speed for that Driver                           
                                DataRow foundrow = drivertrips.Select("DriverName = '" + drivername + "'").FirstOrDefault();
                                if (foundrow == null)//If Trip record comes before Driver record
                                {
                                    foundrow = drivertrips.NewRow();
                                    foundrow["DriverName"] = drivername;
                                    foundrow["Distance"] = distance;
                                    foundrow["Speed"] = speed;
                                    drivertrips.Rows.Add(foundrow);
                                }
                                else if (foundrow != null && (double)foundrow["Distance"] == 0)//Driver record found, Trip record found the first time
                                {
                                    foundrow["Distance"] = distance;
                                    foundrow["Speed"] = speed;
                                }
                                else if ((double)foundrow["Distance"] > 0)//Driver record found, Duplicate Trip record found
                                {
                                    double totaldistance = (double)foundrow["Distance"] + distance;
                                    double totaltime = ((double)foundrow["Distance"] * 60 / (double)foundrow["Speed"]) + (distance * 60 / speed);
                                    double totalspeed = totaldistance * 60 / totaltime;

                                    foundrow["Distance"] = totaldistance;
                                    foundrow["Speed"] = totalspeed;
                                }
                            }
                        }
                    }
                }

                //Sort the table with Distance desc and Print the results
                _helper.SortAndPrintResults(drivertrips);
            }
        }                    
    }    
}
