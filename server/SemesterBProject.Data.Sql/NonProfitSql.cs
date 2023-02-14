using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Dal;
using SemesterBProject.Model;
using Utilities;

namespace SemesterBProject.Data.Sql
{
    public class NonProfitSql: BaseDataSql
    {
        public NonProfitSql(Logger log) : base(log)
        {

        }
        Dictionary<int, NonProfitOrg> OrgDictionary = new Dictionary<int, NonProfitOrg>();
        // Function that insret data to org-post
        public void InsertOrg(object userData, System.Data.SqlClient.SqlCommand command)
        {
            try
            {
                Log.LogEvent("start to get data from client and insert to db");
                if (userData is NonProfitOrg)
                {
                    NonProfitOrg nonProfit = (NonProfitOrg)userData;


                    command.Parameters.AddWithValue("@fullNameRep", nonProfit.FullNameRep);
                    command.Parameters.AddWithValue("@orgName", nonProfit.OrgName);
                    command.Parameters.AddWithValue("@url", nonProfit.URL);
                    command.Parameters.AddWithValue("@email", nonProfit.Email);
                    command.Parameters.AddWithValue("@description", nonProfit.Description);
                    command.Parameters.AddWithValue("@phoneNumber", nonProfit.PhoneNumber);
                }

                int rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }  
            
        }

        public void AddOrgToTbl(NonProfitOrg profitOrg)
        {
            string Insert = "insert into NonProfitOrg values(@fullNameRep,@orgName,@url,@email,@description,@phoneNumber)";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.RunUser(Insert, InsertOrg, profitOrg);
        }

        //read all the from db(get)
        public void AddOrgToDictionary(SqlDataReader reader)
        {
            try
            {
                //clear dictionary
                OrgDictionary.Clear();

                while (reader.Read())
                {
                    NonProfitOrg nonProfit = new NonProfitOrg();

                    nonProfit.OrgID = reader.GetInt32(reader.GetOrdinal("OrgID"));
                    nonProfit.FullNameRep = reader.GetString(reader.GetOrdinal("FullNameRep"));
                    nonProfit.OrgName = reader.GetString(reader.GetOrdinal("OrgName"));
                    nonProfit.URL = reader.GetString(reader.GetOrdinal("URL"));
                    nonProfit.Email = reader.GetString(reader.GetOrdinal("Email"));
                    nonProfit.Description = reader.GetString(reader.GetOrdinal("Description"));
                    nonProfit.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                    Log.LogEvent("read data from db");
                    //add the new Org to dictionary 
                    OrgDictionary.Add(nonProfit.OrgID, nonProfit);
                }
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
           
        }
        public Dictionary<int, NonProfitOrg> GetOrgFromDB()
        {
            string insert = "select * from  NonProfitOrg";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.runCommand(insert, AddOrgToDictionary);
            return OrgDictionary;
        }

        public object LoadOneOrg(System.Data.SqlClient.SqlCommand command, int? OrgId)
        {
            Log.LogEvent($"Load one org {OrgId}");
            try
            {
                if (command == null && (OrgId == null))
                {
                    Log.LogError("command or OrgId are null");
                    return null;
                }
                else
                {
                    command.Parameters.AddWithValue("@orgId", OrgId);
                    NonProfitOrg nonProfit = new NonProfitOrg();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                nonProfit.OrgID = reader.GetInt32(reader.GetOrdinal("OrgID"));
                                nonProfit.FullNameRep = reader.GetString(reader.GetOrdinal("FullNameRep"));
                                nonProfit.OrgName = reader.GetString(reader.GetOrdinal("OrgName"));
                                nonProfit.URL = reader.GetString(reader.GetOrdinal("URL"));
                                nonProfit.Email = reader.GetString(reader.GetOrdinal("Email"));
                                nonProfit.Description = reader.GetString(reader.GetOrdinal("Description"));
                                nonProfit.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                            }
                        }
                    }
                    return nonProfit;
                }
                 
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
            

        }

        public NonProfitOrg Load1Org(string OrgId)
        {
            string select = "select * from [dbo].[NonProfitOrg] where [OrgID]=@orgId";
            SqlQuery sqlQuery = new SqlQuery();
            object OrgObj = sqlQuery.RunOrg(select, LoadOneOrg, int.Parse(OrgId));
           NonProfitOrg nonProfit = null;
            if (OrgObj is NonProfitOrg) { nonProfit = (NonProfitOrg)OrgObj; }
            return nonProfit;

        }



    }
}
