using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int ItemCategoryId { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; } = null!;
        public byte[]? Picture { get; set; }

        public virtual ICollection<Item>? Items { get; set; }
    }
}
