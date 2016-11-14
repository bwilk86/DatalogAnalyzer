using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using DataLogApp.Model;
using DataLogApp.Objects;
using DataLogApp.Properties;
using DataLogApp.Views;
using Utilities.MVVM;
using Utilities.XML;
using UserControl = System.Windows.Controls.UserControl;

namespace DataLogApp.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private UserDataModel _userDataModel;
        private UserSettingsData _userSettingsData;
        private UserControl _contentControl;
        private AppSettings _appSettings;
        
        private ICommand _idleCommand;
        private ICommand _loadSessionCommand;
        private ICommand _saveCommand;

        public MainViewModel()
        {
            _userDataModel = new UserDataModel();

            _userSettingsData = new UserSettingsData();

            _contentControl = new StartingPage {DataContext = this};

            _appSettings = new AppSettings();
        }

        

        public ICommand IdleCommand
        {
            get { return _idleCommand ?? (_idleCommand = new DelegateCommand(Idle)); }
        }

        public UserControl ContentControl
        {
            get { return _contentControl; }
            set
            {
                if (value == null) return;
                _contentControl = value;
                OnPropertyChanged("ContentControl");
            }
        }

        public void Idle(object obj)
        {
            if (string.IsNullOrEmpty(_userSettingsData.TuningProgram))
            {
                if (!ConfigureUserSettings())
                {
                    return;
                }
            }

            ContentControl = new IdleControl() {DataContext = new IdleViewModel(_userDataModel, _userSettingsData, _appSettings, this)};
            ContentControl.UpdateLayout();
        }

        public void LoadUserSettings(object obj)
        {
            var file = new OpenFileDialog
                {
                    InitialDirectory = string.IsNullOrEmpty(_userSettingsData.UserSessionDataPath) ? "c:\\" : _userSettingsData.UserSessionDataPath,
                            Multiselect = false,
                            Filter = Resources.XmlFileFilter,
                            FilterIndex = 2,
                            RestoreDirectory = true
                };

            if (file.ShowDialog() != DialogResult.OK) return;

            if (string.IsNullOrEmpty(file.FileName) || Path.GetExtension(file.FileName) != ".xml") return;

            var xmlHandler = new XmlHandler();
            
            _userSettingsData = xmlHandler.Deserialize<UserSettingsData>(file.FileName);
        }

        public bool ConfigureUserSettings()
        {
            _userSettingsData.TuningProgram = "Cobb";
            return true;
        }
    }
}