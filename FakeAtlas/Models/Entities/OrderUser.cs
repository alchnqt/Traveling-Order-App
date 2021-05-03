using System;
using System.Collections.Generic;

#nullable disable

namespace FakeAtlas
{
    public partial class OrderUser
    {
        public int? IdUser { get; set; }
        public int? IdOrder { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual BookingUser IdUserNavigation { get; set; }
    }
}
