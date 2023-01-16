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
using static System.Collections.Specialized.BitVector32;
using System.Collections.Generic;

namespace SemesterBProject.Azure
{
    public static class ActivistServices
    {
        [FunctionName("ActivistServices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "activist/{action}")] HttpRequest req,
            ILogger log, string action)
        {
            switch (action)
            {
                case "Get":
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    JsonConvert.DeserializeObject(requestBody);

                    Dictionary<int, SocialActivist> ActivistsDictionary = MainManager.Instance.activists.Init();
                    string responseMessage = System.Text.Json.JsonSerializer.Serialize(ActivistsDictionary);

                    return new OkObjectResult(responseMessage);


               // case "GetByID":

                  //  string responseMessage1 = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.products.LoadpRroductById(ProductID));

                   // return new OkObjectResult(responseMessage1);



                case "Post":
                    string requestBody1 = await new StreamReader(req.Body).ReadToEndAsync();
                   SocialActivist activist = System.Text.Json.JsonSerializer.Deserialize<SocialActivist>(requestBody1);
                    if (activist.FirstName != null && activist.LastName != null && activist.Address != null && activist.Email != null && activist.PhoneNumber != null && activist.TwitterAcount != null)
                    {
                        MainManager.Instance.activists.addActivist(activist);

                        return new OkObjectResult("The operation was successful");
                    }
                    return new OkObjectResult("The operation failed");
            }
            return null;
        }
    }
}
