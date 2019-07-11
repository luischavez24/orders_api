using System;
using System.Collections.Generic;

namespace DistributedSystems.Orders.Api.Models
{
    public partial class OrderTracking
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int StatusId { get; set; }
        public DateTime UpdateStatusDate { get; set; }

        public virtual Order Order { get; set; }
        public virtual Status Status { get; set; }
    }
}
