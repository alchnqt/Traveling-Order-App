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
        private UnitOfWork unitOfWork;

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
            
            ProductionWindowFactory factory = new ProductionWindowFactory();
            factory.CreateNewWindow();
            CloseWindow();
            StringBuilder Sb = new();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(UnsecurePassword));
                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            try
            {
                //var accessUser = (from user in unitOfWork.BookingUsers.Get()
                //                  where user.UserLogin == SelectedAccount.UserLogin
                //                  && user.UserPassword == Sb.ToString()
                //                  select user).Single();
                //MessageBox.Show(Sb.ToString());
                LoginView.Close();
                //MessageBox.Show("Connected!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
