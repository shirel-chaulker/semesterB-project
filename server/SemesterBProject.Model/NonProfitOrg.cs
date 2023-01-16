using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterBProject.Model
{
    public class NonProfitOrg
    {
        public int OrgID { get; set; }
        public string FullNameRep { get; set; }
        public string OrgName { get; set; }
        public string URL { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
    }
}
