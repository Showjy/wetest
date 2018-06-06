using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wetest.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string ProviderId { get; set; }

        public string ServicerId { get; set; }

        public long Price { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Information { get; set; }//需求

        public int Progress { get; set; }//进度 0-100  

        public string Status { get; set; }//订单状态  waiting open closed finished
    }
}
