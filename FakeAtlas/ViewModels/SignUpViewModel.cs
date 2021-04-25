using FakeAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAtlas.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private SignUpModel _selectedAccount;

        public SignUpModel SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }
    }
}
