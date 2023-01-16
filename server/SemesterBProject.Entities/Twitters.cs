using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;
using SemesterBProject.Model.Twitter;

namespace SemesterBProject.Entities
{
    public class Twitters
    {
        public List<TwitterTrack> Init()
        {
            Data.Sql.TwitterSql twitterSql = new Data.Sql.TwitterSql();
            return twitterSql.GetTwitterFromDB();
        }

        public void UpdateTrackData(TwitterTrack track, decimal userMoney )
        {
            Data.Sql.TwitterSql twitter = new Data.Sql.TwitterSql();
            twitter.UpdateMoneyUser(track,userMoney);
        }

    }
}
