using System;
using System.Linq;
using System.Data;
using Microsoft.VisualBasic.FileIO;

namespace Utilities.CsvHelper
{
    public class CsvHandler
    {
        public DataTable GetDataTableFromCsvFile(string csvFilePath)
        {
            var csvData = new DataTable();

            try
            {
                using (var csvReader = new TextFieldParser(csvFilePath))
                {
                    csvReader.SetDelimiters(",");
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    
                    //read column names
                    var colFields = csvReader.ReadFields();

                    if (colFields != null)
                        foreach (var datecolumn in colFields.Select(column => new DataColumn(column) {AllowDBNull = true}))
                        {
                            csvData.Columns.Add(datecolumn);
                        }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (var i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                //commented this for later implementation
            }
            return csvData;
        }
    }
}
