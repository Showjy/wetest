using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wetest.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string Price { get; set; }

        public string StartData { get; set; }

        public string EndData { get; set; }

        public string Information { get; set; }//需求

        public string Status { get; set; }
    }
}
