using FakeAtlas.Models;
using FakeAtlas.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FakeAtlas.ViewModels
{
    public class LoginViewModel : BaseViewModel<Account>
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
                OnPropertyChanged(nameof(ResizeBorderThikness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };
        }

        public int ResizeBorder { get; set; } = 6;

        public Thickness ResizeBorderThikness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }
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
    }
}
