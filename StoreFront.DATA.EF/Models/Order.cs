using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipServer { get; set; } = null!;
        public string ShipAccountName { get; set; } = null!;
        public string ShipAccountCountry { get; set; } = null!;

        public virtual User? User { get; set; } = null!;
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
