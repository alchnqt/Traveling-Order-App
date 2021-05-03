using FakeAtlas.ViewModels.Management;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace FakeAtlas.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IViewModel, ICloseWindow, IMinimizeWindow, IMaximizeWindow, IRestoreWindow
    {
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

        #region Basic Commands

        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        protected void CloseWindow()
        {
            Close?.Invoke();
        }
        public bool CanClose()
        {
            return true;
        }
        public Action Close { get; set; }

        private DelegateCommand _minimizeCommand;

        public DelegateCommand MinimizeCommand => _minimizeCommand ?? (_minimizeCommand = new DelegateCommand(MinimizeWindow));

        private void MinimizeWindow()
        {
            Minimize?.Invoke();
        }
        public bool CanMinimize()
        {
            return true;
        }
        public Action Maximize { get; set; }

        private DelegateCommand _maximizeCommand;

        public DelegateCommand MaximizeCommand => _maximizeCommand ?? (_maximizeCommand = new DelegateCommand(MaximizeWindow));

        private void MaximizeWindow()
        {
            Maximize?.Invoke();
            RestoreVisibility = Visibility.Visible;
            MaximizeVisibility = Visibility.Collapsed;
        }
        public bool CanMaximize()
        {
            return true;
        }
        public Action Minimize { get; set; }

        public Action Restore { get; set; }

        private DelegateCommand _restoreCommand;

        public DelegateCommand RestoreCommand => _restoreCommand ?? (_restoreCommand = new DelegateCommand(RestoreWindow));

        private void RestoreWindow()
        {
            Restore?.Invoke();

            RestoreVisibility = Visibility.Collapsed;
            MaximizeVisibility = Visibility.Visible;
        }
        public bool CanRestore()
        {
            return true;
        }

        #endregion
    }
}
