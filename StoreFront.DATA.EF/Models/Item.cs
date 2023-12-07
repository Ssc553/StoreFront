using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Item
    {
        public Item()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ItemId { get; set; }
        public int ItemTypeId { get; set; }
        public string Name { get; set; } = null!;
        public int RequiredLevel { get; set; }
        public int MaxStackSize { get; set; }
        public bool? IsUnique { get; set; }
        public bool? IsSetItem { get; set; }
        public bool? IsSocketed { get; set; }
        public string Rarity { get; set; } = null!;
        public int ItemCategoryId { get; set; }
        public int ServerId { get; set; }
        public short ItemsInStock { get; set; }
        public decimal? Price { get; set; }
        public string? ItemImage { get; set; }

        public virtual Category ItemCategory { get; set; } = null!;
        public virtual ItemType ItemType { get; set; } = null!;
        public virtual ServerMode Server { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
