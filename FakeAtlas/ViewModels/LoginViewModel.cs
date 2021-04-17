using FakeAtlas.Context.UnitOfWork;
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
    public class LoginViewModel : BaseViewModel<Login>
    {
        public Login SelectedAccount
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
        /// <summary>
        /// The margin around the window
        /// </summary>
        private int lOuterMarginSize = 10;
        /// <summary>
        /// The radius of edges of the window 
        /// </summary>
        private int lWindowRadius = 10;
        #endregion
        public LoginViewModel(LoginWindow loginWindow)
        {
            _loginWindow = loginWindow;
            _loginWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };
            LogInCommand = new RelayCommand(o => LoginClick());
            CloseCommand = new RelayCommand(o => CloseClick());
            unitOfWork = new UnitOfWork();
        }

        #region Window properties
        public int ResizeBorder { get; set; } = 6;

        public Thickness ResizeBorderThikness { get { return new Thickness(ResizeBorder); } }
        /// <summary>
        /// The margin around the window
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return _loginWindow.WindowState == WindowState.Maximized ? 0 : lOuterMarginSize;
            }
            set
            {
                lOuterMarginSize = value;
            }
        }
        public Thickness OuterMarginSizeThickness
        {
            get
            {
                return new Thickness(OuterMarginSize);
            }
        }
        /// <summary>
        /// The radius of edges of the window 
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return _loginWindow.WindowState == WindowState.Maximized ? 0 : lWindowRadius;
            }
            set
            {
                lWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius
        {
            get
            {
                return new CornerRadius(OuterMarginSize);
            }
        }
        #endregion

        UnitOfWork unitOfWork;
        public ICommand LogInCommand { get; set; }
        
        private void LoginClick()
        {
            StringBuilder Sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(_loginWindow.tbPassword.Password));
                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            try
            {
                var accessUser = (from user in unitOfWork.BookingUsers.GetAll()
                                  where user.UserLogin == _loginWindow.tbLogin.Text
                                  && user.UserPassword == Sb.ToString()
                                  select user).Single();

                MessageBox.Show("Connected!");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public ICommand CloseCommand { get; set; }

        private void CloseClick()
        {
            _loginWindow.Close();
        }
    }
}
