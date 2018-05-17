using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wetest.Models.relatmodels
{
    public class UserToOrder
    {
        public string Id { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
        public string OrderId { get; set; }

    }
}
