using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FakeAtlas.ViewModels
{
    public class BaseViewModel<T> : INotifyPropertyChanged, IViewModel
    {
        protected T _objectViewModel;
        /// <summary>
        /// A base view model that fires Property Changed events as needed
        /// </summary>      
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));


    }
}
