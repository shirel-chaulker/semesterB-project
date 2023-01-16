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
    public static class ProductServices
    {
        [FunctionName("ProductServices")]
        public static async Task<IActionResult> Products(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "product/{action}/{ProductID?}")] HttpRequest req,string action ,string ProductID,ILogger log)
        {
           switch (action)
            {
                case "Get":
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    JsonConvert.DeserializeObject(requestBody);

                    Dictionary<int, Product> CampaignDictionary = MainManager.Instance.products.Init();
                    string responseMessage = System.Text.Json.JsonSerializer.Serialize(CampaignDictionary);

                    return new OkObjectResult(responseMessage);


                case "GetByID":

                    string responseMessage1 = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.products.LoadpRroductById(ProductID));

                    return new OkObjectResult(responseMessage1);



                case "Post":
                    string requestBody1 = await new StreamReader(req.Body).ReadToEndAsync();
                    Product product = System.Text.Json.JsonSerializer.Deserialize<Product>(requestBody1);
                    if (product.ProductName != null && product.Product_Value !=0  && product.donate_company!= null && product.NonProfitName != null && product.CampaignName != null) 
                    {
                        MainManager.Instance.products.addProduct(product);

                        return new OkObjectResult("The operation was successful");
                    }
                    return new OkObjectResult("The operation failed");


                case "PostPurchase":

                    string requestBody2 = await new StreamReader(req.Body).ReadToEndAsync();
                    Purchase purchase = System.Text.Json.JsonSerializer.Deserialize<Purchase>(requestBody2);
                    if (purchase.ProductName != null && purchase. FullName != null && purchase.Address != null && purchase.PhoneNumber != null && purchase.CampaignDonation != null)
                    {
                        MainManager.Instance.Purchases.PostPurchase(purchase);

                        return new OkObjectResult("The operation was successful");
                    }
                    return new OkObjectResult("The operation failed");




            }
            return null;
        }
    }
}
