using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAtlas.ViewModels
{
    public class BookingUserViewModel : ViewModelBase
    {
        private BookingUser _selectedUser;

        public BookingUser SelectedUser
        {
            get { return _selectedUser; }
            set 
            { 
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        public List<string> orders { get; set; } = new List<string>();
        public BookingUserViewModel()
        {
        
        }
    }
}
