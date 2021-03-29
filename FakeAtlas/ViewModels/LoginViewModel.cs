using FakeAtlas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeAtlas.ViewModels
{
    public class LoginViewModel : ObjectViewModel<Account>
    {
        public Account SelectedAccount
        {
            get { return _objectViewModel; }
            set
            {
                _objectViewModel = value;
                OnPropertyChanged("SelectedAccount");
            }
        }
    }
}
