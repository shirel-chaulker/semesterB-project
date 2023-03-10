using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Dal;
using SemesterBProject.Model;
using Utilities;

namespace SemesterBProject.Data.Sql
{
    public class purchaseSql: BaseDataSql
    {
        public purchaseSql(Logger log) : base(log)
        {

        }
        public void InsertPurchase(object userData, System.Data.SqlClient.SqlCommand command)
        {
            Log.LogEvent("get data from client and insert to db");
            try
            {
                if (userData is Purchase)
                {
                    Purchase purchase = (Purchase)userData;


                    command.Parameters.AddWithValue("@productName", purchase.ProductName);
                    command.Parameters.AddWithValue("@fullName", purchase.FullName);
                    command.Parameters.AddWithValue("@address", purchase.Address);
                    command.Parameters.AddWithValue("@phoneNumber", purchase.PhoneNumber);
                    command.Parameters.AddWithValue("@campaignDonation", purchase.CampaignDonation);

                }

                int rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
           
        }

        public void AddPurchaseToTbl(Purchase purchase)
        {
            string Insert = "insert into [dbo].[Shopping] values(@productName,@fullName,@address,@phoneNumber,@campaignDonation)";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.RunUser(Insert, InsertPurchase, purchase);
        }
    }
}
