using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FakeAtlas.Models
{
    [Table("account")]
    public class Account : ObjectModel
    {
        string _login;
        string _password;

        public Account(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get => _login; set => _login = value; }
        public string Password { get => _password; set => _password = value; }
    }
}
