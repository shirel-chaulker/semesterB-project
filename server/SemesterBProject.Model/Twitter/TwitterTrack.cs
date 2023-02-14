using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterBProject.Model.Twitter
{
    public class TwitterTrack
    {
        public TwitterTrack() { }
        public int TrackID { get; set; }
        public decimal EarnMoney { get; set; }

        public int CampaignID { get; set; }
        public string Hashtag { get; set; }
        public string TwitterAcount { get; set; }

        public DateTime? Date { get; set; }

        public string Answer { get; set; }  
        public string Tweets_Message_Id { get; set; }
    }
}
