using System;
using System.Collections.Generic;

#nullable disable

namespace FakeAtlas.Models
{
#nullable enable
    public partial class BookingUser : ObjectModel
    {
        public BookingUser()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string ?FullName { get; set; }
        public int ?IdAddress { get; set; }
        public bool IsAdmin { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual UniqueAddress IdAddressNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
