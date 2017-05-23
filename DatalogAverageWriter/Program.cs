using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.CsvHelper;
using Utilities.DataTableHelper;

namespace DatalogAverageWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the path to your datalog files");

            var path = Console.ReadLine();

            var csvHandler = new CsvHandler();

            var dataTableHelper = new DataTableHelper();

            var AFR12 = csvHandler.GetDataTableFromCsvFile("\\gears12Afr.csv");
            var AFR34 = csvHandler.GetDataTableFromCsvFile("\\gears34Afr.csv");
            var AFR56 = csvHandler.GetDataTableFromCsvFile("\\gears56Afr.csv");

            DirectoryInfo d = new DirectoryInfo(path);

            var resultsDataTable = new DataTable();

            resultsDataTable.Columns.Add("RPM");
            resultsDataTable.Columns.Add("Calculated Load");
            resultsDataTable.Columns.Add("Measured AFR");
            resultsDataTable.Columns.Add("Target AFR");
            resultsDataTable.Columns.Add("% Difference AFR");
            resultsDataTable.Columns.Add("STFT");
            resultsDataTable.Columns.Add("LTFT");

            string targetAFR = string.Empty;
            
            try
            {
                foreach(var file in d.GetFiles("datalog*.csv"))
                {
                    Console.WriteLine("Which Gear was this file recorded in?");
                    Console.WriteLine(file.FullName);

                    var gear = int.Parse(Console.ReadLine());
                    
                    var dataTable = csvHandler.GetDataTableFromCsvFile(file.FullName);
                    DataTable gearDataTable = new DataTable();

                    if(gear == 1 || gear == 2)
                    {
                        gearDataTable = AFR12;
                    }
                    else if(gear == 3 || gear == 4)
                    {
                        gearDataTable = AFR34;
                    }
                    else
                    {
                        gearDataTable = AFR56;
                    }

                    gearDataTable.PrimaryKey = new DataColumn[] { gearDataTable.Columns["RPM"] };
                    gearDataTable.Columns["RPM"].Unique = true;


                    var RPM = dataTableHelper.RpmBand(dataTable, "RPM (RPM)");                  
                    var averages = dataTableHelper.GetAllColumnAverages(dataTable);

                    RPM = RPM * 1000;
                    
                    foreach (DataColumn col in gearDataTable.Columns)
                    {
                        double convertedColumnName;
                        if (double.TryParse(col.ColumnName, out convertedColumnName))
                        {
                            if(convertedColumnName <= averages["Calculated Load (%)"])
                            {
                               targetAFR = gearDataTable.Select($"RPM == {RPM}").First()[col].ToString();
                            }
                            else
                            {
                                break;
                            }
                        }   
                    }
                    
                    var row = resultsDataTable.NewRow();
                    row["RPM"] = RPM*1000;
                    row["Calculated Load"] = averages["Calculated Load (%)"];
                    row["Measured AFR"] = averages["Equiv. Ratio (AFR)"];
                    row["Target AFR"] = targetAFR;
                    row["% Difference AFR"] = "";//calculate difference;
                    row["STFT"] = averages["Short Term FT (%)"];
                    row["LTFT"] = averages["Long Term FT (%)"];
                }
            }
            catch (Exception e)
            {
                try
                {
                    var dataTable = csvHandler.GetDataTableFromCsvFile(path);

                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
    }
}
