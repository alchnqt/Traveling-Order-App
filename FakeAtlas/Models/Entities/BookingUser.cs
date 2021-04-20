using System;
using System.Collections.Generic;

#nullable disable

namespace FakeAtlas
{
    public partial class BookingUser
    {
        public BookingUser(string login, string password, int id = 0)
        {
            Orders = new HashSet<Order>();
            UserLogin = login;
            UserPassword = password;
            IsAdmin = false;
        }
        public BookingUser()
        {
            Orders = new HashSet<Order>();
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? IdAddress { get; set; }
        public bool IsAdmin { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }

        public virtual UniqueAddress IdAddressNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
