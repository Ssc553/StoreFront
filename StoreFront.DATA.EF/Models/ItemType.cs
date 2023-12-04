﻿using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class ItemType
    {
        public ItemType()
        {
            Items = new HashSet<Item>();
        }

        public int ItemTypeId { get; set; }
        public string ItemTypeName{ get; set; } = null!;

        public virtual ICollection<Item>? Items { get; set; }
    }
}
