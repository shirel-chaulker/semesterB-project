using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Dal;
using SemesterBProject.Model;
using SemesterBProject.Model.Twitter;

namespace SemesterBProject.Data.Sql
{
    public class TwitterSql
    {
        // create dictionary 
       List<TwitterTrack> TwitterList = new List<TwitterTrack>();

        //read from db and inset to dictionary
        public void AddTwitterToDictionary(SqlDataReader reader)
        {

            //clear List
            TwitterList.Clear();

            while (reader.Read())
            {
                TwitterTrack twitter= new TwitterTrack();

                twitter.TrackID = reader.GetInt32(reader.GetOrdinal("TrackID"));
                twitter.EarnMoney = reader.GetDecimal(reader.GetOrdinal("EarnMoney"));
                twitter.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                twitter.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                twitter.TwitterAcount = reader.GetString(reader.GetOrdinal("TwitterAcount"));


                //add to the list
                TwitterList.Add(twitter);
            }

        }

        public List<TwitterTrack> GetTwitterFromDB()
        {
            string insert = "select [dbo].[Track].[TrackID],[dbo].[Track].[EarnMoney],[dbo].[Campaigns].[CampaignID],\r\n[dbo].[Campaigns].[Hashtag],[dbo].[SocialActivist].[TwitterAcount] from [dbo].[Track] inner join [dbo].[Campaigns] on [dbo].[Track].[CampaignID]=[dbo].[Campaigns].[CampaignID]\r\ninner join [dbo].[SocialActivist] on [dbo].[SocialActivist].[ActivistId]=[dbo].[Track].[ActivistId] ";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.runCommand(insert, AddTwitterToDictionary);
            return TwitterList;
        }

        //update Track by userId

        public void UpdateUserTrack(object newData, System.Data.SqlClient.SqlCommand command)
        {
            if (newData is TwitterTrack)
            {
                TwitterTrack twitter = (TwitterTrack) newData;
                command.Parameters.AddWithValue("@TrackID", twitter.TrackID);
                command.Parameters.AddWithValue("@EarnMoney", twitter.EarnMoney);

            }
            int rows = command.ExecuteNonQuery();
        }

        public void UpdateMoneyUser(TwitterTrack twitter, decimal userMoney)
        {
            string Update = "if exists (select * from [dbo].[Track] where [TrackID]=@trackID and [Active]=1)\r\nbegin \r\nupdate [dbo].[Track] set [EarnMoney]=@earnMoney where  [TrackID]=@trackID and [Active]=1\r\nend";

            SqlQuery sqlQuery = new SqlQuery();
            twitter.EarnMoney = twitter.EarnMoney + userMoney;
            sqlQuery.RunUser(Update, UpdateUserTrack, twitter);
        }
    }
}
