using Utilities.MVVM;

namespace DataLogApp.Model
{
    public class UserSettingsData : ObservableObject
    {
        private string _datalogsPath;

        public string DatalogsPath
        {
            get { return _datalogsPath; }
            set
            {
                if (value == null) return;
                _datalogsPath = value;
                OnPropertyChanged("DatalogsPath");
            }
        }

        private string _userSessionDataPath;
        public string UserSessionDataPath
        {
            get { return _userSessionDataPath; }
            set
            {
                if (value == null) return;
                _userSessionDataPath = value;
                OnPropertyChanged("UserSessionDataPath");
            }
        }

        private string _tuningProgram;

        public string TuningProgram
        {
            get { return _tuningProgram; }
            set
            {
                if (value == null) return;
                _tuningProgram = value;
                OnPropertyChanged("TuningProgram");
            }
        }

        
    }
}