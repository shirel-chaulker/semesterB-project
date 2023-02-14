using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterBProject.Model
{
    public class MoneyTrack
    {
        public int TrackID { get; set; }
        public int CampaignID { get; set; }
        public int ActivistId { get; set; }
        public decimal EarnMoney { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
    }
}
