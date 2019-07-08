using System;
using System.Collections.Generic;

namespace DistributedSystems.Project.Purchase.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string FullName {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public virtual ICollection<Order> Order { get; set; }
    }
}
