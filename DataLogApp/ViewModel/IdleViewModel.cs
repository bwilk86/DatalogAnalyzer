using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using DataLogApp.Model;
using DataLogApp.Objects;
using DataLogApp.Properties;
using DataLogApp.Views;
using Utilities;
using Utilities.MVVM;
using Utilities.DataTableHelper;


namespace DataLogApp.ViewModel
{
    class IdleViewModel : ObservableObject
    {
        private string _idlePath;
        private UserDataModel _userDataModel;
        private UserSettingsData _userSettings;
        private Dictionary<string, ColumnNames> _columnNamesDictionary;
        private DataTableHelper _dataTableHelper;
        private AppSettings _appSettings;
        private MainViewModel _mainViewModel;

        
        private ICommand _browseCommand;

        public IdleViewModel()
        {
            _userDataModel = new UserDataModel();
            _userSettings = new UserSettingsData();
            _dataTableHelper = new DataTableHelper();
            _mainViewModel = new MainViewModel();
        }
        public IdleViewModel(UserDataModel userData, UserSettingsData userSettings, AppSettings appSettings, MainViewModel mainViewModel)
        {
            _userDataModel = userData;
            _userSettings = userSettings;
            _dataTableHelper = new DataTableHelper();
            _appSettings = appSettings;
            _mainViewModel = mainViewModel;
        }

        public string IdlePath
        {
            get { return _idlePath; }
            set
            {
                if (value == null) return;
                _idlePath = value;
                OnPropertyChanged("IdlePath");
            }
        }

      private string _averageAirflow;

        public string AverageAirflow
        {
            get
            {
                return _averageAirflow;
            }
            set
            {
                _averageAirflow = value;
                OnPropertyChanged("AverageAirflow");
            }
        }
        private string _averageStFt;

        public string AverageStFt
        {
            get
            {
                return _averageStFt;
            }
            set
            {
                if (value == null) return;
                _averageStFt = value;
                OnPropertyChanged("AverageStFt");
            }
        }

        private string _averageLtFt;
        public string AverageLtFt
        {
            get
            {
                return _averageLtFt;
            }
            set
            {
                if (value == null) return;
                _averageLtFt = value;
                OnPropertyChanged("AverageLtFt");
            }
        }

        private string _averageTotalFt;
        public string AverageTotalFt
        {
            get
            {
                return _averageTotalFt;
            }
            set
            {
                if (value == null) return;
                _averageTotalFt = value;
                OnPropertyChanged("AverageTotalFt");
            }
        }

        private ICommand _analyzeCommand;

        public ICommand AnalyzeCommand
        {
            get { return _analyzeCommand ?? (_analyzeCommand = new DelegateCommand(Analyze)); }
        }

        public void Analyze(object obj)
        {
            if (string.IsNullOrEmpty(_idlePath)) return;

            _userDataModel.AddLogByPath(9, _idlePath);
            var idleDataTable = _userDataModel.IdleLog;

            var columnNamesArray = _appSettings.ColumnNames[_userSettings.TuningProgram];

            var averages = _dataTableHelper.GetAllColumnAverages(idleDataTable);

            var rpm = averages[columnNamesArray.Rpm];

            if (rpm < 800 || rpm > 1250)
            {
                AnalysisMessage =
                    string.Format(
                        "RPM reported in log outside of normal idle RPM\n Normal idle is typically between 800 and 1000 RPM\n Yours is {0}",
                        rpm.ToString("N0"));
                return;
            }

            var avgAirflow = averages[columnNamesArray.MassAirflowRate];

            AverageAirflow = avgAirflow.ToString("N2");

            
            if (avgAirflow  < 5.0 || avgAirflow  > 5.5)
            {
                AirflowResultsColor = Brushes.Red;
                double percentToAdjust = 5.25/avgAirflow ;

                AnalysisMessage =
                    string.Format(
                        "Average Airflow at idle is outside of normal values.\n" +
                        "Recommend you adjust your MAF, scaling the rate of airflow by {0}",
                        percentToAdjust.ToString("N3"));
            }


            if (avgAirflow  >= 5 && avgAirflow  <= 5.5)
            {
                AirflowResultsColor = Brushes.DarkGreen;

                var avgLtft = averages[columnNamesArray.LongTerm];
                AverageLtFt = avgLtft.ToString("N2");
                LtFtResultsColor = Math.Abs(avgLtft) < 3 ? Brushes.DarkGreen : Brushes.Red;
                

                var avgStft = averages[columnNamesArray.ShortTerm];
                AverageStFt = avgStft.ToString("N2");
                StFtResultsColor = Math.Abs(avgStft) < 3 ? Brushes.DarkGreen : Brushes.Red;


                var total = avgStft + avgLtft;
                AverageTotalFt = total.ToString("N2");
                AverageResultsColor = Math.Abs(total) < 5 ? Brushes.DarkGreen : Brushes.Red;
                
                //TODO: move tolerance to settings
                if (avgLtft >= -3.5 && avgLtft <= 3.5 && avgStft >= -3.5 && avgStft <= 3.5 && total >= -3.5 &&
                    total <= 3.5)
                {
                    //Show/save results, move to stage 2
                    Bank1Command.Execute(null);
                }
            }
        }

        private ICommand _bank1Command;

        public ICommand Bank1Command
        {
            get { return _bank1Command ?? (_bank1Command = new DelegateCommand(Bank1)); }
        }

        public void Bank1(object obj)
        {
            _mainViewModel.ContentControl = new InjectorBank1Control
            {
                DataContext = new Bank1ViewModel(_userSettings, _userDataModel,_dataTableHelper, _appSettings ),
                
            };
            
        }

        private string _analysisMessage;

        public string AnalysisMessage
        {
            get { return _analysisMessage;}
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                _analysisMessage = value;
                OnPropertyChanged("AnalysisMessage");
            }
        }

        private SolidColorBrush _airflowResultsColor;

        public SolidColorBrush AirflowResultsColor
        {
            get { return _airflowResultsColor; }
            set
            {
                if (value == null) return;
                _airflowResultsColor = value;
                OnPropertyChanged("AirflowResultsColor");
            }
        }

        private SolidColorBrush _stFtResultsColor;

        public SolidColorBrush StFtResultsColor
        {
            get { return _stFtResultsColor; }
            set
            {
                if (value == null) return;
                _stFtResultsColor = value;
                OnPropertyChanged("StFtResultsColor");
            }
        }

        private SolidColorBrush _ltFtResultsColor;

        public SolidColorBrush LtFtResultsColor
        {
            get { return _ltFtResultsColor; }
            set
            {
                if (value == null) return;
                _ltFtResultsColor = value;
                OnPropertyChanged("LtFtResultsColor");
            }
        }

        private SolidColorBrush _averageResultsColor;

        public SolidColorBrush AverageResultsColor
        {
            get { return _averageResultsColor; }
            set
            {
                if (value == null) return;
                _averageResultsColor = value;
                OnPropertyChanged("AverageResultsColor");
            }
        }

        public ICommand BrowseCommand
        {
            get { return _browseCommand ?? (_browseCommand = new DelegateCommand(Browse)); }
        }

        public void Browse(object obj)
        {
            var file = new OpenFileDialog
            {
                InitialDirectory = string.IsNullOrEmpty(_userSettings.DatalogsPath) ? "c:\\" : _userSettings.DatalogsPath,
                Multiselect = false,
                Filter = Resources.IdleViewModel_FileFilter,
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (file.ShowDialog() != DialogResult.OK) return;

            if (string.IsNullOrEmpty(file.FileName) || Path.GetExtension(file.FileName) != ".csv") return;

            IdlePath = file.FileName;
        }
    }
}
