using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterBProject.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Product_Value { get; set; }
        public string donate_company { get; set; }
        public string NonProfitName { get; set; }
        public string CampaignName { get; set; }
    }
}
