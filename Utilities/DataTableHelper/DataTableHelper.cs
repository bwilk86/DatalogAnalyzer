using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Utilities;

namespace Utilities.DataTableHelper
{
    public class DataTableHelper
    {
        public double GetColumnAverage(DataTable dataTable, string columnName)
        {
            var enumberableTable = dataTable.AsEnumerable();
            return enumberableTable.Average((row) => Convert.ToDouble(row[columnName]));
        }

        public Dictionary<string, double> GetAllColumnAverages(DataTable datatable)
        {
            var enumerableTable = datatable.AsEnumerable();

            var averagesDictionary = datatable.Columns.Cast<DataColumn>().ToDictionary(column => column.ColumnName, column => enumerableTable.Average((row) => Convert.ToDouble(row[column.ColumnName])));
            
            return averagesDictionary;
        }

        public int RpmBand(DataTable dataTable, string columnName)
        {
            var enumerableTable = dataTable.AsEnumerable();
            var rpmAverage = Convert.ToInt32(enumerableTable.Average((row) => Convert.ToDouble(row[columnName])));

            if (rpmAverage >= 1250 && rpmAverage < 1750)
            {
                return 15;
            }

            if (rpmAverage >= 1250 && rpmAverage < 1750)
            {
                return 15;
            }

            if (rpmAverage >= 1750 && rpmAverage < 2250)
            {
                return 20;
            }
            
            if (rpmAverage >= 2250 && rpmAverage < 2750)
            {
                return 25;
            }

            if (rpmAverage >= 2750 && rpmAverage < 3250)
            {
                return 30;
            }

            if (rpmAverage >= 3250 && rpmAverage < 3750)
            {
                return 35;
            }

            if (rpmAverage >= 3750 && rpmAverage < 4250)
            {
                return 40;
            }

            if (rpmAverage >= 4250 && rpmAverage < 4750)
            {
                return 45;
            }

            if (rpmAverage >= 4750 && rpmAverage < 5250)
            {
                return 50;
            }

            if (rpmAverage >= 5250 && rpmAverage < 5750)
            {
                return 55;
            }

            if (rpmAverage >= 5750 && rpmAverage < 6250)
            {
                return 60;
            }

            if (rpmAverage >= 6250 && rpmAverage < 6750)
            {
                return 65;
            }

            if (rpmAverage >= 6750 && rpmAverage < 7250)
            {
                return 70;
            }

            if (rpmAverage >= 7250 && rpmAverage < 7750)
            {
                return 75;
            }

            if (rpmAverage >= 7750 && rpmAverage < 8250)
            {
                return 80;
            }

            if (rpmAverage >= 8250 && rpmAverage < 8750)
            {
                return 85;
            }

            if (rpmAverage >= 8750 && rpmAverage < 9250)
            {
                return 90;
            }

            return 0;
        }

        //TODO: Implement method to determine the gear that the datalog was logged in using RPM/Vehicle Speed Average
        //public int DataLogGear(DataTable dataTable, string rpmColumnName, string speedColumnName)
        //{
        //    var averageTotal = dataTable.AsEnumerable().Sum(row => Convert.ToDouble(row[speedColumnName])/Convert.ToInt32(row[rpmColumnName]));

        //    var gearRatio = averageTotal/dataTable.Rows.Count;


        //}
    }
}
