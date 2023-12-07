using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public decimal ItemPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public int OrderDetailsId { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
