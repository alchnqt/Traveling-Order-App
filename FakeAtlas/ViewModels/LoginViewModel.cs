using FakeAtlas.Context.Repository;
using FakeAtlas.Context.UnitOfWork;
using FakeAtlas.CustomUIElements;
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
    public class LoginViewModel : BaseViewModel<Login>
    {
        #region Private members
        private LoginWindow _loginWindow;

        private UnitOfWork unitOfWork;

        private Visibility _isSignUpVisible = Visibility.Collapsed;
        private Visibility _isBacwardVisible = Visibility.Collapsed;
        private Visibility _isWindowVisible = Visibility.Visible;

        #endregion

        #region Bindings

        
        public Visibility WindowVisibility
        {
            get { return _isWindowVisible; }
            set
            {
                _isWindowVisible = value;
                OnPropertyChanged(nameof(WindowVisibility));
            }
        }
        public Visibility BackwardVisibility
        {
            get { return _isBacwardVisible; }
            set
            {
                _isBacwardVisible = value;
                OnPropertyChanged(nameof(BackwardVisibility));
            }
        }
        public Visibility SignUpVisibility
        {
            get { return _isSignUpVisible; }
            set
            {
                _isSignUpVisible = value;
                OnPropertyChanged(nameof(SignUpVisibility));
            }
        }

        public Login SelectedAccount
        {
            get { return _objectViewModel; }
            set
            {
                _objectViewModel = value;
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }
        #endregion

        public LoginViewModel(LoginWindow loginWindow)
        {

            _loginWindow = loginWindow;
            _loginWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(SelectedAccount.UserLogin));
                OnPropertyChanged(nameof(SignUpVisibility));
                OnPropertyChanged(nameof(BackwardVisibility));
            };
            SignInCommand = new RelayCommand(o => SignInClick());
            SignUpCommand = new RelayCommand(o => SignUpClick());
            ShowSignUpCommand = new RelayCommand(o => ShowSignUpClick());
            HideSignUpCommand = new RelayCommand(o => HideSignUpClick());
            unitOfWork = new UnitOfWork();
        }

        #region Commands


        public ICommand SignInCommand { get; set; }
        
        private void SignInClick()
        {
            ProductionWindowFactory factory = new ProductionWindowFactory();
            factory.CreateNewWindow();
            CloseWindow();
            //StringBuilder Sb = new StringBuilder();
            //using (var hash = SHA256.Create())
            //{
            //    Encoding enc = Encoding.UTF8;
            //    Byte[] result = hash.ComputeHash(enc.GetBytes(_loginWindow.tbPasswordBox.Password));
            //    foreach (Byte b in result)
            //        Sb.Append(b.ToString("x2"));
            //}
            //try
            //{
            //    var accessUser = (from user in unitOfWork.BookingUsers.Get()
            //                      where user.UserLogin == _objectViewModel.UserLogin
            //                      && user.UserPassword == Sb.ToString()
            //                      select user).Single();

            //    MessageBox.Show("Connected!");
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}
        }
        public ICommand ShowSignUpCommand { get; set; }
        private void ShowSignUpClick()
        {
            SignUpVisibility = Visibility.Visible;
            BackwardVisibility = Visibility.Visible;
        }
        public ICommand SignUpCommand { get; set; }
        private void SignUpClick()
        {
            try
            {
                unitOfWork.BookingUsers.Create(new BookingUser {UserLogin = _objectViewModel.UserLogin, UserPassword = "1234"});
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public ICommand HideSignUpCommand { get; set; }
        private void HideSignUpClick()
        {
            SignUpVisibility = Visibility.Collapsed;
            BackwardVisibility = Visibility.Collapsed;
        }
        #endregion
    }
}
