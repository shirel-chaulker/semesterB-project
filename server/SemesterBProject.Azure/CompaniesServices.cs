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
using SemesterBProject.Model.Reports;
using Newtonsoft.Json.Linq;
using RestSharp;
using SemesterBProject.Model.Twitter;

namespace SemesterBProject.Azure
{
    public static class CompaniesServices
    {
        [FunctionName("CompaniesServices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "companies/{action}")] HttpRequest req,
            ILogger log,string action)
        {

            switch (action)
            {
                case "getCompanies":
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    JsonConvert.DeserializeObject(requestBody);

                    Dictionary<int, BusinessCompany> CompaniesDictionary = MainManager.Instance.BusinessComp.Init();
                    string responseMessage = System.Text.Json.JsonSerializer.Serialize(CompaniesDictionary);

                    return new OkObjectResult(responseMessage);

                case "getReport":

                    string requestBody1 = await new StreamReader(req.Body).ReadToEndAsync();
                    JsonConvert.DeserializeObject(requestBody1);

                    List<DeliveryTrack> DeliveryList = MainManager.Instance.BusinessComp.GetDeliveries();
                    string responseMessage1 = System.Text.Json.JsonSerializer.Serialize(DeliveryList);

                    return new OkObjectResult(responseMessage1);


                case "getAcountData":
                    string requestBody2 = await new StreamReader(req.Body).ReadToEndAsync();
                    JsonConvert.DeserializeObject(requestBody2);

                    List<TwitterTrack> TwitterList = MainManager.Instance.twitters.Init();
                    string responseMessage2 = System.Text.Json.JsonSerializer.Serialize(TwitterList);

                    return new OkObjectResult(responseMessage2);

            }
            return null;
        }
    }
}
