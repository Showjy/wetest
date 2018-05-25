using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wetest.Models.viewmodels
{
    public class OrderCreateViewModel
    {
        public string ProviderId { get; set; }

        public long Price { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Information { get; set; }//需求

    }
}
