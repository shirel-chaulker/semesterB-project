using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterBProject.Model
{
    public class PostsTrack
    {
        public int PostsTrack_Id { get; set; }
        public int TrackId { get; set; }
        public int CampaignID { get; set; }
        public int ActivistId { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }
        public string Tweets_Message { get; set; }
        public int Retweets_Count { get; set; }
        public string Tweets_message_Id { get; set; }
    }
}
