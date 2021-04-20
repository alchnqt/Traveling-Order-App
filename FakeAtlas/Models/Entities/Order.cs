using System;
using System.Collections.Generic;

#nullable disable

namespace FakeAtlas
{
    public partial class Order
    {
        public int Id { get; set; }
        public string PathName { get; set; }
        public int? ClientId { get; set; }
        public int? ShipperId { get; set; }
        public DateTime? OrderTime { get; set; }

        public virtual BookingUser Client { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}
