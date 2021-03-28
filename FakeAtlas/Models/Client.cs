using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FakeAtlas.Models
{
    [Table("client")]
    public class Client : ObjectModel
    {
        private string _fullName;
        private int _idxAddress;
        private string _clientLogin;

        public Client(string fullName, int idxAddress, string clientLogin)
        {
            FullName = fullName;
            IdxAddress = idxAddress;
            ClientLogin = clientLogin;
        }

        public string FullName { get => _fullName; set => _fullName = value; }
        public int IdxAddress { get => _idxAddress; set => _idxAddress = value; }
        public string ClientLogin { get => _clientLogin; set => _clientLogin = value; }
    }
}
