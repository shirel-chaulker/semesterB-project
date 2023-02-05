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
            bool Flag = true;
            while (Flag) 
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string Answer = reader["Answer"].ToString();
                        switch (Answer) 
                        {
                            case "No tweets and valid":
                                DateTime? Date = reader.GetDateTime(reader.GetOrdinal("ActivistDatemoney"));
                                string AnswerTweet = reader.GetString(reader.GetOrdinal("Answer"));

                                AddDataTwitter(reader,Date,AnswerTweet);

                                break;

                            case "No tweets and invalid":
                                DateTime? Date1 = null;
                                string AnswerTweet1 = reader.GetString(reader.GetOrdinal("Answer"));

                                AddDataTwitter(reader, Date1, AnswerTweet1);

                                break;

                            case "Exist tweets and valid":
                                DateTime? Date2 = reader.GetDateTime(reader.GetOrdinal("TweetDate"));
                                string AnswerTweet2 = reader.GetString(reader.GetOrdinal("Answer"));

                                AddDataTwitter(reader, Date2, AnswerTweet2);

                                break;

                            case "Exist tweets and invalid":
                                DateTime? Date3 = null;
                                string AnswerTweet3 = reader.GetString(reader.GetOrdinal("Answer"));

                                AddDataTwitter(reader, Date3, AnswerTweet3);

                                break;
                        }
                    }
                }
                if (!reader.NextResult())
                {
                    Flag = false;
                    

                }
            }

            


        }

        public void AddDataTwitter(SqlDataReader reader,DateTime? date,string answer ) 
        {
            //clear List
            TwitterList.Clear();

            while (reader.HasRows)
            {
                TwitterTrack twitter = new TwitterTrack();

                twitter.TrackID = reader.GetInt32(reader.GetOrdinal("TrackID"));
                twitter.EarnMoney = reader.GetDecimal(reader.GetOrdinal("EarnMoney"));
                twitter.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                twitter.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                twitter.TwitterAcount = reader.GetString(reader.GetOrdinal("TwitterAcount"));
                twitter.Date = date;
                twitter.Answer = answer;


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
