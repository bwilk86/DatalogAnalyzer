using System.Collections.Generic;
using System.Data;
using Utilities.CsvHelper;
using Utilities.MVVM;

namespace DataLogApp.Model
{
    public class UserDataModel : ObservableObject
    {
        private readonly CsvHandler _csvHandler;
        private DataTable _mafCalibration;

        private readonly Dictionary<int, DataTable> _dataTableDictionary; 
       
        public UserDataModel()
        {
            _csvHandler = new CsvHandler();

            _dataTableDictionary = new Dictionary<int, DataTable>
            {
                {9, new DataTable()},
                {15, new DataTable()},
                {20, new DataTable()},
                {25, new DataTable()},
                {30, new DataTable()},
                {35, new DataTable()},
                {40, new DataTable()},
                {45, new DataTable()},
                {50, new DataTable()},
                {55, new DataTable()},
                {60, new DataTable()},
                {65, new DataTable()},
                {70, new DataTable()},
                {75, new DataTable()},
                {80, new DataTable()},
                {85, new DataTable()},
                {90, new DataTable()}
            };
        }

        public DataTable IdleLog
        {
            get { return _dataTableDictionary[9]; }
            set
            {
                if (value != null)
                {
                    _dataTableDictionary[9] = value;
                    OnPropertyChanged("IdleLog");
                }
            }
        }

        public DataTable GetTableByRpm(int key)
        {
            return _dataTableDictionary[key];
        }

        public void SetDataTableByRpm(int key, DataTable dataTable)
        {
            _dataTableDictionary[key] = dataTable;
        }

        /// <summary>
        /// DataTable handlers, used predominantly when using the multiselect capability and allowing the program to determine the RPM band
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataTable"></param>
        public void AddLog(int key, DataTable dataTable)
        {
            _dataTableDictionary[key]= dataTable;
        }
        /// <summary>
        /// CSV Path log handlers
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
       public void AddLogByPath(int key, string path)
        {
            _dataTableDictionary[key] = _csvHandler.GetDataTableFromCsvFile(path);
        }
        
    }
}
