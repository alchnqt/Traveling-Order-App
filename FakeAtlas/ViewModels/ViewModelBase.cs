using FakeAtlas.ViewModels.Management;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace FakeAtlas.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IViewModel, ICloseWindow, IMinimizeWindow, IMaximizeWindow, IRestoreWindow
    {
        /// <summary>
        /// Localization capture enum
        /// </summary>
        public enum Localization
        {
            ru_RU = 0x000,
            en_GB = 0x001
        }

        public static Localization CurrentLocalization { get; set; }

        public ViewModelBase()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            if(currentCulture.Name == "ru-RU")
            {
                CurrentLocalization = Localization.ru_RU;
            }
            else
            {
                CurrentLocalization = Localization.en_GB;
            }
        }

        /// <summary>
        /// A base view model that fires Property Changed events as needed
        /// </summary>      
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));


        private Visibility restoreVisibility = Visibility.Collapsed;
        private Visibility maximizeVisibility = Visibility.Visible;

        public Visibility RestoreVisibility
        {
            get { return restoreVisibility; }
            set
            {
                restoreVisibility = value;
                OnPropertyChanged(nameof(RestoreVisibility));
            }
        }
        public Visibility MaximizeVisibility
        {
            get { return maximizeVisibility; }
            set
            {
                maximizeVisibility = value;
                OnPropertyChanged(nameof(MaximizeVisibility));
            }
        }

        private WindowState _currentState = WindowState.Normal;  

        public WindowState CurrentState
        {
            get { return _currentState; }
            set 
            { 
                _currentState = value;
                if (_currentState == WindowState.Maximized) 
                {
                    RestoreVisibility = Visibility.Visible;
                    MaximizeVisibility = Visibility.Collapsed;
                }
                else if(_currentState == WindowState.Normal)
                {
                    RestoreVisibility = Visibility.Collapsed;
                    MaximizeVisibility = Visibility.Visible;
                }
                OnPropertyChanged(nameof(CurrentState)); 

            }
        }


        #region Basic Commands


        private ICommand select_enGB_Command;
        public ICommand Select_enGB_Command => select_enGB_Command ??= new DelegateCommand(Select_enGB_);

        private void Select_enGB_()
        {
            var rsrc = "Localization/lang.en-GB.xaml";
            var currentRsrc = new Uri(rsrc, UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = currentRsrc };
            CurrentLocalization = Localization.en_GB;
        }

        private ICommand select_ruRU_Command;
        public ICommand Select_ruRU_Command => select_ruRU_Command ??= new DelegateCommand(Select_ruRU_);

        private void Select_ruRU_()
        {
            var rsrc = "Localization/lang.ru-RU.xaml";
            var currentRsrc = new Uri(rsrc, UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = currentRsrc };
            CurrentLocalization = Localization.ru_RU;
        }
        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand => _closeCommand ??= new DelegateCommand(CloseWindow);

        protected void CloseWindow() => Close?.Invoke();

        public bool CanClose() => true;

        public Action Close { get; set; }

        private DelegateCommand _minimizeCommand;

        public DelegateCommand MinimizeCommand => _minimizeCommand ??= new DelegateCommand(MinimizeWindow);

        private void MinimizeWindow() => Minimize?.Invoke();

        public bool CanMinimize() => true;

        public Action Maximize { get; set; }

        private DelegateCommand _maximizeCommand;

        public DelegateCommand MaximizeCommand => _maximizeCommand ??= new DelegateCommand(MaximizeWindow);

        private void MaximizeWindow()
        {
            Maximize?.Invoke();
            RestoreVisibility = Visibility.Visible;
            MaximizeVisibility = Visibility.Collapsed;
        }
        public bool CanMaximize() => true;

        public Action Minimize { get; set; }

        public Action Restore { get; set; }

        private DelegateCommand _restoreCommand;

        public DelegateCommand RestoreCommand => _restoreCommand ??= new DelegateCommand(RestoreWindow);

        private void RestoreWindow()
        {
            Restore?.Invoke();

            RestoreVisibility = Visibility.Collapsed;
            MaximizeVisibility = Visibility.Visible;
        }

        public bool CanRestore() => true;

        #endregion
    }
}
