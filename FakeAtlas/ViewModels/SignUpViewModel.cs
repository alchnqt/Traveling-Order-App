using FakeAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using FakeAtlas.Encryption;
using FakeAtlas.Context.UnitOfWork;
using FakeAtlas.ViewModels.Management;
using System.Windows;

namespace FakeAtlas.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private BookingUser _selectedAccount = new();

        public BookingUser SelectedAccount
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
            set { _unsecurePassword = value; OnPropertyChanged(nameof(UnsecurePassword)); }
        }


        private ICommand signUpCommand;
        public ICommand SignUpCommand => signUpCommand ??= new DelegateCommand(SignUp);

        private void SignUp()
        {
            var salt = AtlasCrypto.GetSalt();
            byte[] result = AtlasCrypto.GenerateSaltedHash(Encoding.UTF8.GetBytes(UnsecurePassword), salt);
            try
            {
                using (UnitOfWork unitOfWork = new())
                {
                    unitOfWork.BookingUsers.Create(new BookingUser { UserLogin = SelectedAccount.UserLogin, UserPassword = Convert.ToBase64String(result), 
                        Salt = Convert.ToBase64String(salt)
                    });
                    unitOfWork.UniqueAddress.Create(new UniqueAddress());
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
