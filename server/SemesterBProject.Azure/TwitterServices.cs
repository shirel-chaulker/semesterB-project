using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SemesterBProject.Entities;
using System.Collections.Generic;
using SemesterBProject.Model;
using SemesterBProject.Model.Twitter;

namespace SemesterBProject.Azure
{
    public static class TwitterServices
    {
        [FunctionName("TwitterServices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Twitter/{action}")] HttpRequest req,
            ILogger log, string action)
        {
            switch (action)
            {
                case "getTwitt":

                    List<TwitterTrack> TwitterAcount = MainManager.Instance.twitters.Init();

                    DateTime currentDate = DateTime.Today;
                    DateTime dateOfTomorrow = DateTime.Today.AddDays(0);
                    string currentDay = currentDate.ToString("yyyy-MM-dd");
                    string tomorrow = dateOfTomorrow.ToString("yyyy-MM-dd");
                    string start_time = currentDay + "T00:00:00Z";
                    string end_time = tomorrow + "T22:50:50Z";

                    foreach (TwitterTrack user in TwitterAcount)
                    {
                        string url = $"https://api.twitter.com/2/tweets/search/recent?start_time={start_time}&end_time={end_time}&query=from:{user.TwitterAcount}";
                        var clientTwitter = new RestClient(url);
                        var requestTwitter = new RestRequest("", Method.Get);
                        requestTwitter.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAA9rkwEAAAAAfVYMxYP3kMZPoIhPNyZEWjvZ5JA%3DawYVw4PgDFl1q3UU5lzpx9imCEbTiupJGYzEDvxYSxMb86iS2h");
                        var responseTwitter = clientTwitter.Execute(requestTwitter);
                        if (responseTwitter.IsSuccessful)
                        {
                            JObject json = JObject.Parse(responseTwitter.Content);
                            int tweetCount = 0;
                            int resultCount = (int)json["meta"]["result_count"];
                            if (resultCount != 0)
                            {
                                foreach (var tweet in json["data"])
                                {
                                    if (tweet["text"].ToString().Contains(user.Hashtag))
                                    {
                                        tweetCount++;
                                    }
                                }
                                Console.WriteLine(tweetCount);
                                MainManager.Instance.twitters.UpdateTrackData(user, tweetCount);
                            }
                        }
                    }
                    return new OkObjectResult("failedNotFollowing");
            }
            return null;
        }
    }
}
