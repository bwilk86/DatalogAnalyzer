using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using DataLogApp.Model;
using DataLogApp.Objects;
using DataLogApp.Properties;
using DataLogApp.Views;
using Utilities.CsvHelper;
using Utilities.DataTableHelper;
using Utilities.MVVM;
using Utilities.XML;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace DataLogApp.ViewModel
{
    public class Bank1ViewModel : ObservableObject
    {
        private bool _useMultiSelect;
        private UserSettingsData _userSettings;
        private UserDataModel _userDataModel;
        private DataTableHelper _dataTableHelper;
        private XmlHandler _xmlHandler;
        private CsvHandler _csvHandler;
        private AppSettings _appSettings;

        public Bank1ViewModel()
        {
            UseMultiSelect = true;
        }

        public Bank1ViewModel(UserSettingsData userSettings, UserDataModel userData, DataTableHelper dataTableHelper,
             AppSettings appSettings)
        {
            UseMultiSelect = true;
            _userSettings = userSettings;
            _userDataModel = userData;
            _dataTableHelper = dataTableHelper;
            _appSettings = appSettings;
        }

        private Visibility _useMultiSelectVisibility;

        public Visibility UseMultiSelectVisibility
        {
            get { return _useMultiSelectVisibility;}
            set
            {
                _useMultiSelectVisibility = value;
                OnPropertyChanged("UseMultiSelectVisibility");
            }
        }

        private string _singleOrMultiSelectText;

        public string SingleOrMultiSelectText
        {
            get { return _singleOrMultiSelectText; }
            set
            {
                _singleOrMultiSelectText = value;
                OnPropertyChanged("SingleOrMultiSelectText");
            }
        }

        public bool UseMultiSelect
        {
            get { return _useMultiSelect; }

            set
            {
                _useMultiSelect = value;
                RpmSelectionMethod = value ? (UserControl) new RpmAutoSelection() : new RpmRangeSelection();
                SingleOrMultiSelectText = value
                    ? "Pick All log files, and we'll tell you which RPM band they are for"
                    : "Please select each log file accordingly";
                OnPropertyChanged("UseMultiSelect");
            }
        }

        private UserControl _rpmSelectionMethod;

        public UserControl RpmSelectionMethod
        {
            get { return _rpmSelectionMethod; }
            set
            {
                if (value == null) return;
                _rpmSelectionMethod = value;
                OnPropertyChanged("RpmSelectionMethod");
            }
        }

        private ICommand _browseCommand;

        public ICommand BrowseSingleOrMulti
        {
            get { return _browseCommand ?? (_browseCommand = new DelegateCommand(BrowseSingleOrMultiSelect)); }
        }

        public bool NotUseMultiSelect { get { return !_useMultiSelect; } }

        public void BrowseSingleOrMultiSelect(object obj)
        {
            var files = new OpenFileDialog()
            {
                InitialDirectory = "c:\\",
                    //string.IsNullOrEmpty(_userSettings.DatalogsPath) ? "c:\\" : _userSettings.DatalogsPath,
                Multiselect = _useMultiSelect,
                Filter = Resources.IdleViewModel_FileFilter,
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (files.ShowDialog() != DialogResult.OK) return;

            if (string.IsNullOrEmpty(files.FileNames.ToString()))
            {
                return;
            }

            var validLogFiles = files.FileNames.Where(f => Path.GetExtension(f) == ".csv");

            var invalidLogFiles = files.FileNames.Except(validLogFiles);

            if (invalidLogFiles.Any())
            {
                MessageBox.Show(
                    string.Format("The following files are invalid. Files must be CSV files {0}", invalidLogFiles),
                    "Invalid Files detected", MessageBoxButtons.OK);
            }



            foreach (var file in validLogFiles)
            {
                var rpmColumnName = _appSettings.ColumnNames[_userSettings.TuningProgram].Rpm;

                var dataTable = _csvHandler.GetDataTableFromCsvFile(file);

                var rpmBand = _dataTableHelper.RpmBand(dataTable, rpmColumnName);

                _userDataModel.AddLog(rpmBand, dataTable);
            }
        }
    }
}
