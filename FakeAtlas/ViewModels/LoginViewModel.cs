using FakeAtlas.Context.Repository;
using FakeAtlas.Context.UnitOfWork;
using FakeAtlas.CustomUIElements;
using FakeAtlas.Models;
using FakeAtlas.Views;
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
    public class LoginViewModel : BaseViewModel<BookingUser>
    {
        public BookingUser SelectedAccount
        {
            get { return _objectViewModel; }
            set
            {
                _objectViewModel = value;
                OnPropertyChanged("SelectedAccount");
            }
        }

        #region Private members
        private LoginWindow _loginWindow;
        private UnitOfWork unitOfWork;
        private BookingDBRepository<BookingUser> userRepository;
        #endregion
        public LoginViewModel(LoginWindow loginWindow)
        {
            _loginWindow = loginWindow;
            _loginWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(SelectedAccount.UserLogin));
            };
            SignInCommand = new RelayCommand(o => SignInClick());
            CloseCommand = new RelayCommand(o => CloseClick());
            MinimizeCommand = new RelayCommand(o => MinimizeClick());
            SignUpCommand = new RelayCommand(o => SignUpClick());
            ShowSignUpCommand = new RelayCommand(o => ShowSignUpClick());
            unitOfWork = new UnitOfWork();
        }
        public ICommand SignInCommand { get; set; }
        
        private void SignInClick()
        {
            StringBuilder Sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(_loginWindow.tbPasswordBox.Password));
                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            try
            {
                var accessUser = (from user in unitOfWork.BookingUsers.Get()
                                  where user.UserLogin == _objectViewModel.UserLogin
                                  && user.UserPassword == Sb.ToString()
                                  select user).Single();

                MessageBox.Show("Connected!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public ICommand CloseCommand { get; set; }

        private void CloseClick()
        {
            _loginWindow.Close();
        }

        public ICommand MinimizeCommand { get; set; }

        private void MinimizeClick()
        {
            _loginWindow.WindowState = WindowState.Minimized;
        }
        public ICommand ShowSignUpCommand { get; set; }
        private void ShowSignUpClick()
        {
            _loginWindow.pageSignUp.Visibility = Visibility.Visible;
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
    }
}
