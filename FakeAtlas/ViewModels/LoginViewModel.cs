using FakeAtlas.Context.Repository;
using FakeAtlas.Context.UnitOfWork;
using FakeAtlas.Models;
using FakeAtlas.ViewModels.Management;
using FakeAtlas.Views;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FakeAtlas.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        private Visibility _isToSignUpVisible = Visibility.Visible;
        private Visibility _isToSignInVisible = Visibility.Collapsed;


        private ViewModelBase _selectedViewModel;

        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set 
            { 
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
               
            }
        }

        #region Bindings


        public Visibility ToSignUpVisibility
        {
            get { return _isToSignUpVisible; }
            set
            {
                _isToSignUpVisible = value;
                OnPropertyChanged(nameof(ToSignUpVisibility));
            }
        }
        public Visibility ToSignInVisibility
        {
            get { return _isToSignInVisible; }
            set
            {
                _isToSignInVisible = value;
                OnPropertyChanged(nameof(ToSignInVisibility));
            }
        }

        #endregion

        public LoginViewModel()
        {
            SignInCommand = new RelayCommand(o => SignInClick());
            SignUpCommand = new RelayCommand(o => SignUpClick());
            _selectedViewModel = new SignInViewModel();
            SignInViewModel.LoginView = this;
            
        }

        #region Commands


        public ICommand UpdateViewCommand { get; set; }

        public ICommand SignInCommand { get; set; }
        
        private void SignInClick()
        {
            ToSignUpVisibility = Visibility.Visible;
            ToSignInVisibility = Visibility.Collapsed;
            SelectedViewModel = new SignInViewModel();
        }

        public ICommand SignUpCommand { get; set; }

        private void SignUpClick()
        {
            SelectedViewModel = new SignUpViewModel();
            ToSignUpVisibility = Visibility.Collapsed;
            ToSignInVisibility = Visibility.Visible;
        }
        #endregion
    }
}
