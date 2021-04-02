using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FakeAtlas.ViewModels
{
    public class BaseViewModel<T> : INotifyPropertyChanged
    {
        protected T _objectViewModel;

        public ObservableCollection<T> ObjectsViewModel { get; set; }

        public void Add(T _object) => ObjectsViewModel.Add(_object);

        public void Remove(T _object) => ObjectsViewModel.Remove(_object);


        /// <summary>
        /// A base view model that fires Property Changed events as needed
        /// </summary>      
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
