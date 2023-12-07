using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public string UserId { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AccountName { get; set; }
        public string? Server { get; set; }
        public string? Country { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
