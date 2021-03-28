using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FakeAtlas.ViewModels
{
    public abstract class ObjectViewModel<T> : INotifyPropertyChanged
    {
        protected T _objectViewModel;

        public ObservableCollection<T> ObjectsViewModel { get; set; }

        public void Add(T _object) => ObjectsViewModel.Add(_object);

        public void Remove(T _object) => ObjectsViewModel.Remove(_object);

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
