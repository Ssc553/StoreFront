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
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string Server { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
