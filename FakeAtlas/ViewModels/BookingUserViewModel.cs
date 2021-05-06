using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FakeAtlas.Context.UnitOfWork;
using Microsoft.VisualStudio.PlatformUI;

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

        private UniqueAddress _selectedAddress;

        public UniqueAddress SelectedAddress
        {
            get { return _selectedAddress; }
            set { _selectedAddress = value; OnPropertyChanged(nameof(SelectedAddress)); }
        }


        public BookingUserViewModel()
        {
            SelectedUser = MainWindowViewModel.User;
            SelectedAddress = MainWindowViewModel.Address;
        }

        private ICommand saveCommand;
        public ICommand SaveCommand => saveCommand ??= new DelegateCommand(Save);

        private void Save()
        {
            try
            {
                using (UnitOfWork unit = new())
                {
                    unit.UniqueAddress.Update(SelectedAddress);
                    unit.Save();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
