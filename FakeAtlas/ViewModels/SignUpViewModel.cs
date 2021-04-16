using FakeAtlas.Context.UnitOfWork;
using FakeAtlas.CustomUIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FakeAtlas.ViewModels
{
    public class SignUpViewModel : BaseViewModel<BookingUser>
    {
        private SignUpPage _signUpWindow;

        UnitOfWork unitOfWork;

        public SignUpViewModel(SignUpPage signUpPage)
        {
            _signUpWindow = signUpPage;
            SignUpCommand = new RelayCommand(o => SignUpClick());
            unitOfWork = new UnitOfWork();
        }
        public BookingUser SelectedAccount
        {
            get { return _objectViewModel; }
            set
            {
                _objectViewModel = value;
                OnPropertyChanged("SelectedAccount");
            }
        }
        public ICommand SignUpCommand { get; set; }
        private void SignUpClick()
        {
            StringBuilder Sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(_signUpWindow.tbPassword.Password));
                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            //automapper
            unitOfWork.BookingUsers.Create(new BookingUser(_signUpWindow.tbLogin.Text, Sb.ToString()));
            unitOfWork.Save();
        }
    }
}
