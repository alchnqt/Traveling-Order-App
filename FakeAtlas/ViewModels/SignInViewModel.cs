using FakeAtlas.Context.UnitOfWork;
using FakeAtlas.Models;
using FakeAtlas.ViewModels.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FakeAtlas.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly UnitOfWork unitOfWork;

        private SignInModel _selectedAccount;

        public SignInModel SelectedAccount
        {
            get { return _selectedAccount; }
            set 
            { 
                _selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }

        private string _unsecurePassword;

        public string UnsecurePassword
        {
            get { return _unsecurePassword; }
            set 
            {
                _unsecurePassword = value;
                OnPropertyChanged(nameof(UnsecurePassword));
            }
        }
        public static LoginViewModel LoginView { get; set; }

        public SignInViewModel()
        {
            unitOfWork = new UnitOfWork();
            SignInCommand = new RelayCommand(o => SignInClick());
        }

        public ICommand SignInCommand { get; set; }

        private void SignInClick()
        {
            StringBuilder Sb = new();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(UnsecurePassword));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            try
            {
                MainWindowViewModel.User = (from user in unitOfWork.BookingUsers.Get()
                                  where user.UserLogin == SelectedAccount.UserLogin
                                  && user.UserPassword == Sb.ToString()
                                  select user).Single();
                ProductionWindowFactory factory = new();
                factory.CreateNewWindow();
                CloseWindow();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
