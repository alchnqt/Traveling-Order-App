using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FakeAtlas.Models
{
    [Table("administrator")]
    public class Admin : ObjectModel
    {
        private string _login;
        private string _password;
        private int _idx;

        public Admin(string login, int idx, string password)
        {
            Login = login;
            Idx = idx;
            _password = password;
        }

        public string Login { get => _login; set => _login = value; }
        public int Idx { get => _idx; set => _idx = value; }
        public string Password { get => _password; set => _password = value; }
    }
}
