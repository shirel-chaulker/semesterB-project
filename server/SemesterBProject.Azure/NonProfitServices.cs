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
using System.Security.Cryptography;

namespace SemesterBProject.Azure
{
    public static class NonProfitServices
    {
        [FunctionName("NonProfitServices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "nonProfit/{action}/{OrgID?}")] HttpRequest req,
            ILogger log,string action,string OrgID)
        {
           switch (action) 
           {
                case "Get":
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    JsonConvert.DeserializeObject(requestBody);

                    Dictionary<int, NonProfitOrg> OrgDictionary = MainManager.Instance.NonProfit.Init();
                    string responseMessage = System.Text.Json.JsonSerializer.Serialize(OrgDictionary);

                    return new OkObjectResult(responseMessage);

                case "GetByID":

                    string responseMessage1 = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.NonProfit.LoadOrgById(OrgID));

                    return new OkObjectResult(responseMessage1);



                case "Post":
                    string requestBody1 = await new StreamReader(req.Body).ReadToEndAsync();
                    NonProfitOrg nonProfit = System.Text.Json.JsonSerializer.Deserialize<NonProfitOrg>(requestBody1);
                    if (nonProfit.FullNameRep != null && nonProfit.OrgName != null && nonProfit.URL != null && nonProfit.Email != null && nonProfit.Description != null && nonProfit.PhoneNumber != null)
                    {
                        MainManager.Instance.NonProfit.PostNonProfit(nonProfit);

                        return new OkObjectResult("The operation was successful");
                    }
                    return new OkObjectResult("The operation failed");
           } 
            return null;
        }
    }
}
