using System;
using System.Collections.Generic;
using Utilities;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;
using SemesterBProject.Model.Twitter;

namespace SemesterBProject.Entities
{
    public class Twitters: BaseEntity
    {
       
        public Twitters(Logger log):base(log)
        {
           
        }
        
        public List<TwitterTrack> Init()
        {
            Data.Sql.TwitterSql twitterSql = new Data.Sql.TwitterSql(Log);
            return twitterSql.GetTwitterFromDB();
        }

        public void UpdateTrackData(TwitterTrack track, decimal userMoney )
        {
            Data.Sql.TwitterSql twitter = new Data.Sql.TwitterSql(Log);
            twitter.UpdateMoneyUser(track,userMoney);
        }

    }
}
