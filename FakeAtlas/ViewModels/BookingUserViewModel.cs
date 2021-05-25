﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FakeAtlas.Context.UnitOfWork;
using FakeAtlas.Encryption;
using FakeAtlas.Models.Entities;
using FakeAtlas.ViewModels.Management;
using Microsoft.VisualStudio.PlatformUI;

namespace FakeAtlas.ViewModels
{
    public class BookingUserViewModel : ViewModelBase
    {

        private string _unsecurePassword;

        public string UnsecurePassword
        {
            get { return _unsecurePassword; }
            set { _unsecurePassword = value; OnPropertyChanged(nameof(UnsecurePassword)); }
        }


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

        private bool ValidatePassword(string password)
        {
            if (!string.IsNullOrEmpty(UnsecurePassword))
                return passwordRegex.IsMatch(UnsecurePassword);
            else
                return false;

        }

        private void Save()
        {
            try
            {
                if(ValidatePassword(UnsecurePassword))
                {
                    byte[] salt = AtlasCrypto.GetSalt();
                    SelectedUser.UserPassword = Convert.ToBase64String(AtlasCrypto.GenerateSaltedHash(Encoding.UTF8.GetBytes(UnsecurePassword), salt));
                    SelectedUser.Salt = Convert.ToBase64String(salt);
                }
                using (UnitOfWork unit = new())
                {
                    unit.AddressRepository.Update(SelectedAddress);
                    unit.BookingUsers.Update(SelectedUser);
                    unit.Save();
                }
            }
            catch (Exception e)
            {
                FakeAtlasMessageBoxService box = new();
                box.ShowMessage(e.Message);
            }
        }
    }
}
