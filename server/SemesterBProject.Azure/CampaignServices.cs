using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SemesterBProject.Entities;
using SemesterBProject.Model;
using System.Collections.Generic;

namespace SemesterBProject.Azure
{
    public static class CampaignServices
    {
        [FunctionName("Campaign")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post","delete","put", Route = "campaign/{action}/{CampaignId?}")] HttpRequest req,
            ILogger log,string action,string CampaignID)
        {
            switch (action)
            {
               
                case "Add":
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Campaign campaign1 = System.Text.Json.JsonSerializer.Deserialize<Campaign>(requestBody);
                    if (campaign1.CampaignName != null && campaign1.NonProfitName  != null && campaign1.Hashtag != null && campaign1.Description != null)
                    {
                        MainManager.Instance.campaigns.addCampaign(campaign1);

                        return new OkObjectResult("The operation was successful");
                    }
                    return new OkObjectResult("The operation failed");


                case "Get":

                    string requestBody1 = await new StreamReader(req.Body).ReadToEndAsync();
                    JsonConvert.DeserializeObject(requestBody1);
                        
                        Dictionary<int, Campaign> CampaignDictionary = MainManager.Instance.campaigns.Init();
                        string responseMessage = System.Text.Json.JsonSerializer.Serialize(CampaignDictionary);

                        return new OkObjectResult(responseMessage);

                case "GetByID":
                        
                        string responseMessage1 = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaigns.LoadCampaignById(CampaignID));

                        return new OkObjectResult(responseMessage1);

                    

                  

                case "UpdateCampaign":
                    string requestBody2 = await new StreamReader(req.Body).ReadToEndAsync();

                    Campaign campaign = System.Text.Json.JsonSerializer.Deserialize<Campaign>(requestBody2);


                    if (campaign.CampaignID != 0 && campaign.CampaignName != null && campaign.Hashtag != null && campaign.Description != null && campaign.NonProfitName != null)
                    {
                        MainManager.Instance.campaigns.UpdateCampaign(campaign);

                        return new OkObjectResult("The operation was successful");
                    }
                    return new OkObjectResult("The operation failed");

                case "DeleteCampaign":


                    if (!(CampaignID == null))
                    {
                        MainManager.Instance.campaigns.Deletecampaign(CampaignID);

                        return new OkObjectResult("The operation was successful");
                    }

                    return new OkObjectResult("The operation failed");


            }

            return null;
        }
    }
}

