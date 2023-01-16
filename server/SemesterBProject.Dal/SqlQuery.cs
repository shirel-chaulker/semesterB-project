using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Model;
using static SemesterBProject.Dal.SqlQuery;

namespace SemesterBProject.Dal
{
    public class SqlQuery
    {
        string connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SemesterBProject;Data Source=localhost\SQLEXPRESS";
        SqlConnection connection;
        public SqlQuery()
        {
            connection = new SqlConnection(connectionString);
        }

        public bool Connect()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {

                return false;
            }
        }


        //public delegate object SetResultDataReader_delegate(SqlDataReader reader);

        ////Function to get Data from SQL and returns an Object
        //public static object RunCommandResult(string sqlQuery, SetResultDataReader_delegate func)
        //{
        //    object ret = null;
        //    string connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SemesterBProject;Data Source=localhost\SQLEXPRESS";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string queryString = sqlQuery;

        //        //Adapter
        //        using (SqlCommand command = new SqlCommand(queryString, connection))
        //        {
        //            connection.Open();

        //            //Reader
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                ret = func(reader);
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //Function to Insert/Update/Delete Data into/from DB (SQL)
        //public static void RunNonQuery(string sqlQuery)
        //{
        //    string connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SemesterBProject;Data Source=localhost\SQLEXPRESS";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string queryString = sqlQuery;

        //        //Adapter
        //        using (SqlCommand command = new SqlCommand(queryString, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}


        //Create a delegate to insert function (post=add)
        public delegate void postDataReader_delegate(Campaign campaign, SqlCommand command);
        //function that insert data to table 
        public void RunAddCampaign(string sqlQuerey, postDataReader_delegate func, Campaign campaign)
        {
            if (!Connect()) return;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                func(campaign, command);

            }

        }




        //create delegate 
        public delegate void SetDataReader_delegate(SqlDataReader reader);

        //Get data from sql and return an object
        public void runCommand(string sqlQuerey, SetDataReader_delegate func)
        {
            if (!Connect()) return;


            string insert = sqlQuerey;

            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        func(reader);
                    }
                }
            }
        }

        //Create delegate for update function 
        public delegate void UpdateCampaign_delegate(SqlCommand command, Campaign campaign);

        //Function that can update the db
        public void RunUpdateCampaign(string sqlQuerey, UpdateCampaign_delegate func, Campaign campaign)
        {
            if (!Connect()) return;
            string insert = sqlQuerey;

            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                func(command, campaign);

            }

        }
        //Add ? for int to get null || function for load one campaign from db by id 
        public delegate void deleteCampaign_delegate(SqlCommand command, int? campaignId);
        public void RunDeleteCampaign(string sqlQuerey, deleteCampaign_delegate func, int campaignId)
        {
            if (!Connect()) return;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                func(command, campaignId);

            }

        }
        public delegate object set_delegate(SqlCommand command, int? campaignId);
        public object RunCampaign(string sqlQuerey, set_delegate func, int campaignId)
        {
            object obj = null;
            if (!Connect()) return null;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
               obj= func(command, campaignId);

            }
            return obj;

        }

        public delegate void postProduct_delegate(Product product, SqlCommand command);
        //function that insert data to table 
        public void RunAddProduct(string sqlQuerey, postProduct_delegate func, Product product)
        {
            if (!Connect()) return;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                func(product, command);

            }

        }

        public delegate object setProduct_delegate(SqlCommand command, int? ProductId);
        public object RunProduct(string sqlQuerey, setProduct_delegate func, int ProductId)
        {
            object obj = null;
            if (!Connect()) return null;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                obj = func(command, ProductId);

            }
            return obj;

        }

        public delegate void postActivist_delegate(SocialActivist activist, SqlCommand command);
        //function that insert data to table 
        public void RunAddActivist(string sqlQuerey, postActivist_delegate func, SocialActivist activist)
        {
            if (!Connect()) return;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                func(activist, command);

            }

        }

        public delegate object GetId_delegate(SqlCommand command, int? OrgId);
        public object RunOrg(string sqlQuerey, GetId_delegate func, int OrgId)
        {
            object obj = null;
            if (!Connect()) return null;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                obj = func(command, OrgId);

            }
            return obj;

        }

        // generic sqlQuery -post
        //create delegate
        public delegate void PostData_delegate(object userData, SqlCommand command);

        public void RunUser(string sqlQuerey, PostData_delegate func, object userData)
        {
            if (!Connect()) return;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                func(userData, command);

            }
            
        }

    }
}
