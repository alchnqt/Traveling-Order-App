using System;
using System.Collections.Generic;

#nullable disable

namespace FakeAtlas
{
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public int? VehicleNum { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
