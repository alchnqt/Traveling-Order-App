using FakeAtlas.ViewModels.Management;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FakeAtlas.ViewModels
{
    public class BaseViewModel<T> : INotifyPropertyChanged, IViewModel, ICloseWindow, IMinimizeWindow
    {
        protected T _objectViewModel;
        /// <summary>
        /// A base view model that fires Property Changed events as needed
        /// </summary>      
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(CloseWindow));

        private void CloseWindow()
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
        public Action Minimize { get; set; }
    }
}
