using FakeAtlas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeAtlas.ViewModels
{
    public class AdminViewModel : BaseViewModel<Admin>
    {
        public Admin SelectedAdmin
        {
            get { return _objectViewModel; }
            set
            {
                _objectViewModel = value;
                OnPropertyChanged("SelectedAdmin");
            }
        }
    }
}
