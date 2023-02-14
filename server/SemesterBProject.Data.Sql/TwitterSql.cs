using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SemesterBProject.Dal;
using SemesterBProject.Model;
using SemesterBProject.Model.Twitter;
using Utilities;

namespace SemesterBProject.Data.Sql
{
    public class TwitterSql: BaseDataSql
    {
        public TwitterSql(Logger log) : base(log)
        {

        }
        // create list 
       List<TwitterTrack> TwitterList = new List<TwitterTrack>();

        //read from db and inset to dictionary
        public void AddTwitterToDictionary(SqlDataReader reader)
        {
            Log.LogEvent("Tests cases in a db");

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
                                try
                                {
                                    DateTime? Date = reader.GetDateTime(reader.GetOrdinal("MoneyDateActive"));
                                    string AnswerTweet = reader.GetString(reader.GetOrdinal("Answer"));

                                    AddDataTwitter(reader, Date, AnswerTweet);
                                }
                                catch (Exception ex)
                                {
                                    Log.LogException("An exception occurred:", ex);
                                    throw;
                                }
                               

                                break;

                            case "No tweets and invalid":
                                try
                                {
                                    
                                    DateTime? Date1 = null;
                                    string AnswerTweet1 = reader.GetString(reader.GetOrdinal("Answer"));

                                    AddDataTwitter(reader, Date1, AnswerTweet1);
                                }
                                catch (Exception ex)
                                {
                                    Log.LogException("An exception occurred:", ex);
                                    throw;
                                }

                                break;

                            case "Exist tweets and valid":
                                try
                                {
                                    DateTime? Date2 = reader.GetDateTime(reader.GetOrdinal("PostsDateActive"));
                                    string AnswerTweet2 = reader.GetString(reader.GetOrdinal("Answer"));

                                    AddDataTwitter(reader, Date2, AnswerTweet2);
                                }
                                catch (Exception ex)
                                {
                                    Log.LogException("An exception occurred:", ex);
                                    throw;
                                }
                              
                                break;

                            case "Exist tweets and invalid":
                                try
                                {
                                    DateTime? Date3 = null;
                                    string AnswerTweet3 = reader.GetString(reader.GetOrdinal("Answer"));

                                    AddDataTwitter(reader, Date3, AnswerTweet3);
                                }
                                catch (Exception ex)
                                {
                                    Log.LogException("An exception occurred:", ex);
                                    throw;
                                }
                               
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
            Log.LogEvent("clear TwitterList");
            //clear List
            TwitterList.Clear();

            try
            {
                if (reader.HasRows)
                {
                    TwitterTrack twitter = new TwitterTrack();

                    twitter.TrackID = reader.GetInt32(reader.GetOrdinal("TrackId"));
                    twitter.Tweets_Message_Id = reader["Tweets_Message_Id"].ToString();
                    twitter.EarnMoney = reader.GetDecimal(reader.GetOrdinal("EarnMoney"));
                    twitter.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                    twitter.TwitterAcount = reader.GetString(reader.GetOrdinal("Twitter_user"));
                    twitter.Date = date;
                    twitter.Answer = answer;

                    Log.LogEvent("add to the list");
                    //add to the list
                    TwitterList.Add(twitter);
                }
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
          
        }

        public List<TwitterTrack> GetTwitterFromDB()
        {
            string insert = "AllUserDetails";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.runCommand(insert, AddTwitterToDictionary);
            return TwitterList;
        }

        //update Track by userId

        public void UpdateUserTrack(object newData, System.Data.SqlClient.SqlCommand command)
        {
            Log.LogEvent("update the money track of user by twitter api");
            try
            {
                if (newData is TwitterTrack)
                {
                    TwitterTrack twitter = (TwitterTrack)newData;
                    command.Parameters.AddWithValue("@TrackID", twitter.TrackID);
                    command.Parameters.AddWithValue("@EarnMoney", twitter.EarnMoney);

                }
                int rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
           
        }

        public void UpdateMoneyUser(TwitterTrack twitter, decimal userMoney)
        {
            try
            {
                string Update = "if exists (select * from [dbo].[Track] where [TrackID]=@trackID and [Active]=1)\r\nbegin \r\nupdate [dbo].[Track] set [EarnMoney]=@earnMoney where  [TrackID]=@trackID and [Active]=1\r\nend";

                SqlQuery sqlQuery = new SqlQuery();
                twitter.EarnMoney = twitter.EarnMoney + userMoney;
                sqlQuery.RunUser(Update, UpdateUserTrack, twitter);
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
            
        }

        public void PostTracking(object newData, System.Data.SqlClient.SqlCommand command)
        {
            // Get a list of newData
            if (newData != null)
            {
                List<PostsTrack> TweetsList = null;

                if (newData is List<PostsTrack>)
                {
                    TweetsList = (List<PostsTrack>)newData;
                }


                // Convert the list to a DataTable
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("TrackID", typeof(int));
                dataTable.Columns.Add("CampaignID", typeof(int));
                dataTable.Columns.Add("ActivistId", typeof(int));
                dataTable.Columns.Add("Date", typeof(DateTime));
                dataTable.Columns.Add("Active", typeof(bool));
                dataTable.Columns.Add("Tweets_Message", typeof(string));
                dataTable.Columns.Add("Retweets_Count", typeof(int));
                dataTable.Columns.Add("Tweets_Message_Id", typeof(string));
                try
                {
                    if (TweetsList.Count > 0 && TweetsList != null)
                    {
                        foreach (PostsTrack item in TweetsList)
                        {
                            dataTable.Rows.Add(item.TrackId, item.CampaignID, item.ActivistId, item.Date, 1, item.Tweets_Message, item.Retweets_Count, item.Tweets_message_Id);
                        }

                        // Execute the stored procedure and pass the DataTable as a parameter
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TempTweetsTable", dataTable);
                        int val = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }

        string insertNoTweetsList = "AddFirstTweets";
        string insertExistTweetsList = "AddTweets";
        public void PostPostsTracking(List<PostsTrack> NoTweetsList,
        List<PostsTrack> ExistTweetsList)
        {
            try
            {
                SqlQuery sql = new SqlQuery();
                if (NoTweetsList.Count() > 0)
                {
                    sql.RunUser(insertNoTweetsList, PostTracking, NoTweetsList);
                }
                if (ExistTweetsList.Count() > 0)
                {
                    sql.RunUser(insertExistTweetsList, PostTracking, ExistTweetsList);
                }
            }
            catch (Exception EX)
            {
                throw;
            }
        }
    }
}
