using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wetest.Models.models
{
    public class Payment
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProviderId { get; set; }
        public string ServicerId { get; set; }
        public int Price { get; set; }
        public string ClosedDate{ get; set; }
        public string AppealDate { get; set; }
        public string Information { get; set; }
    }
}
