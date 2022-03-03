using CSISD_Tolling_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment.Models
{
    public class IndexViewModel
    {
        public List<Invoice> invoices { get; set; }
        public string userId { get; set; }
    }
}
