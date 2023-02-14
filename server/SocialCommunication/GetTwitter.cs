using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;
using SemesterBProject.Model.Twitter;
using Utilities;

namespace SocialCommunication
{
    public class GetTwitter : BaseSocialCommuniction
    {
        TwitterSql twitterSql;
        
       
        Task TwitterTask = null;
        bool StopLoop { get; set; } = false;

        public delegate string UrlQueryDelegate(object data);
        public delegate void AccessDataSqlDelegate(object date, object newDate);

        public GetTwitter(Logger log):base(log)
        {
            twitterSql = new TwitterSql(base.Log);
            PostTweets();
        }

        string UrlQuery(object user)
        {
            TwitterTrack User = null;
            string url = null;
            try
            {
                if (user != null)
                {
                    if (user is TwitterTrack)
                    {
                        User = user as TwitterTrack;

                        if (User.Answer.Contains("No tweets"))
                        {
                            url = $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={User.TwitterAcount}&count=800&trim_user=t";
                        }
                        else if (User.Answer.Contains("Exist tweets"))
                        {
                            url = $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={User.TwitterAcount}&count=800&trim_user=t&since_id={User.Tweets_Message_Id}";
                        }
                    }
                }
            }
            catch (Exception EX)
            {

                throw;
            }
            return url;
        }

      


        public void PostTweets()
        {
            TwitterTask = Task.Run(() =>
            {
                while (!StopLoop)
                {
                    try
                    {
                        List<TwitterTrack> UserData = twitterSql.GetTwitterFromDB();
                        List<PostsTrack> NoTweetsList = new List<PostsTrack>();
                        List<PostsTrack> ExistTweetsList = new List<PostsTrack>();



                        if (UserData != null && UserData.Count > 0)
                        {
                            foreach (TwitterTrack user in UserData)
                            {
                                string Url = UrlQuery(user);
                                if (Url != null)
                                {

                                    var clientTwitter = new RestClient(Url);
                                    var requestTwitter = new RestRequest("", Method.Get);
                                    requestTwitter.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAA9rkwEAAAAAfVYMxYP3kMZPoIhPNyZEWjvZ5JA%3DawYVw4PgDFl1q3UU5lzpx9imCEbTiupJGYzEDvxYSxMb86iS2h");
                                    var responseTwitter = clientTwitter.Execute(requestTwitter);
                                    if (responseTwitter.IsSuccessful)
                                    {
                                        JArray json = JArray.Parse(responseTwitter.Content);
                                        int tweetCount = 0;
                                        int AccumulatedMoney = 0;
                                        string tweetTime;
                                        string TweetsMessage;
                                        string TweetsMessageId;
                                        if (json.Count > 0)
                                        {
                                            foreach (var tweet in json)
                                            {
                                                if (tweet != null)
                                                {
                                                    TweetsMessage = tweet["text"].ToString();
                                                    if (!TweetsMessage.Contains("RT @")) //לעשות תנאי שבודק שלא מדובר בציוץ שמשהו שיתף
                                                    {
                                                        if (TweetsMessage.Contains(user.Hashtag))
                                                        {
                                                            tweetTime = tweet["created_at"].ToString();
                                                            TweetsMessageId = tweet["id"].ToString();
                                                            int retweet_count = int.Parse(tweet["retweet_count"].ToString());
                                                            tweetCount++;
                                                            AccumulatedMoney = AccumulatedMoney + retweet_count + 1;
                                                            PostsTrack tBPostsTracking = new PostsTrack
                                                            {
                                                                TrackId = user.TrackID,
                                                                Date = ChangeStringToDateTime(tweetTime),
                                                                Retweets_Count = retweet_count,
                                                                Tweets_Message = TweetsMessage,
                                                                Tweets_message_Id = TweetsMessageId,

                                                            };
                                                            if (user.Answer.Contains("No tweets"))
                                                            {
                                                                NoTweetsList.Add(tBPostsTracking);
                                                            }
                                                            else if (user.Answer.Contains("Exist tweets"))
                                                            {
                                                                ExistTweetsList.Add(tBPostsTracking);
                                                            }
                                                        }
                                                    }
                                                }
                                                Console.WriteLine(tweetCount);
                                                if (tweetCount != 0)
                                                {
                                                    twitterSql.UpdateMoneyUser(user, AccumulatedMoney);
                                                }

                                            }
                                            if (NoTweetsList.Count > 0 || ExistTweetsList.Count > 0)
                                            {
                                                twitterSql.PostPostsTracking(NoTweetsList, ExistTweetsList);
                                                NoTweetsList.Clear();
                                                ExistTweetsList.Clear();
                                            }
                                        }
                                    }
                                }
                                Thread.Sleep(1000 * 60 * 60);
                            }
                        }
                    }
                    catch (Exception EX)
                    {

                        throw;
                    }
                }
            });
        }


        public DateTime ChangeStringToDateTime(string dateInput)
        {
            DateTime TweetTime;
            try
            {
                string dateInputWithComma = dateInput.Replace(' ', ',');
                string[] DateArray = dateInputWithComma.Split(new string[] { "," }, StringSplitOptions.None);
                string DateTweet = $"{DateArray[1]} {DateArray[2]},{DateArray[5]}";
                string TimeTweet = $"{DateArray[3]}";

                var parsedDate = DateTime.Parse(DateTweet);
                var parsedTime = DateTime.Parse(TimeTweet).AddHours(+2);

                TweetTime = new DateTime(parsedDate.Year, parsedDate.Month, parsedDate.Day, parsedTime.Hour, parsedTime.Minute, 0);
            }
            catch (Exception EX)
            {

                throw;
            }
            return TweetTime;
        }
    }
}

