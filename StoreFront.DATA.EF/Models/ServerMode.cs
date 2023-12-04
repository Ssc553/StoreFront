using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class ServerMode
    {
        public ServerMode()
        {
            Items = new HashSet<Item>();
        }

        public int ServerId { get; set; }
        public string ServerName { get; set; } = null!;
        public string ModeName { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }
    }
}
