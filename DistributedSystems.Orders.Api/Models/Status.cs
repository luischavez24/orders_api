using System;
using System.Collections.Generic;

namespace DistributedSystems.Orders.Api.Models
{
    public partial class Status
    {
        public Status()
        {
            OrderTracking = new HashSet<OrderTracking>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<OrderTracking> OrderTracking { get; set; }
    }
}
