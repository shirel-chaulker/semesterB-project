using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SemesterBProject.Model;
using SemesterBProject.Dal;
using Utilities;

namespace SemesterBProject.Data.Sql
{
    public class CampaignSql: BaseDataSql
    {
        public CampaignSql(Logger log) : base(log)
        {

        }
       
        //create dictionary
        Dictionary<int, Campaign> CampaignsDictionary = new Dictionary<int, Campaign>();
        // Function that insret data to campaign
        public void InsertCampaign(Campaign campaign, System.Data.SqlClient.SqlCommand command)
        {
            Log.LogEvent("start to get data from client and insert to db");
            try
            {
                command.Parameters.AddWithValue("@campaignName", campaign.CampaignName);
                command.Parameters.AddWithValue("@NonProfitName", campaign.NonProfitName);
                command.Parameters.AddWithValue("@hashtag", campaign.Hashtag);
                command.Parameters.AddWithValue("@description", campaign.Description);
                int rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
            
        }

        public void AddCampaignToTbl(Campaign campaign)
        {
           
            string Insert = " insert into Campaigns values (@campaignName,@NonProfitName,@hashtag,@description)";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.RunAddCampaign(Insert, InsertCampaign, campaign);
        }


        public void AddCampignToDictionary(SqlDataReader reader)
        {
            Log.LogEvent("read from campaign table");
            try
            {
                CampaignsDictionary.Clear();

                while (reader.Read())
                {
                    Campaign campaign = new Campaign();

                    campaign.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                    campaign.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                    campaign.NonProfitName = reader.GetString(reader.GetOrdinal("NonProfitName"));
                    campaign.Description = reader.GetString(reader.GetOrdinal("Description"));
                    campaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));

                    Log.LogEvent("add campaign to dictionary");
                    //add the new campaign to dictionary 
                    CampaignsDictionary.Add(campaign.CampaignID, campaign);
                }
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
           

        }
        public Dictionary<int, Campaign> GetCampaignsFromDB()
        {
            Log.LogEvent("call to dal function and get data from db");
            string insert = "select * from Campaigns";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.runCommand(insert, AddCampignToDictionary);
            return CampaignsDictionary;
        }






        //function that update the campaign 
        public void UpdateCampaign(System.Data.SqlClient.SqlCommand command, Campaign campaign)
        {
            try
            {
                if (command == null && (campaign == null))
                {
                    Log.LogError("command or camapign are null");
                    return;
                }
                else
                {

                 command.Parameters.AddWithValue("@campaignId", campaign.CampaignID);
                    command.Parameters.AddWithValue("@campaignName", campaign.CampaignName);
                    command.Parameters.AddWithValue("@NonProfitName", campaign.NonProfitName);
                    command.Parameters.AddWithValue("@hashtag", campaign.Hashtag);
                    command.Parameters.AddWithValue("@description", campaign.Description);
                    int rows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.LogException("faild to update the data on db", ex);
                throw;

            }
        }
        //function i send to sqlquery
        public void EditCampaign(Campaign campaign)
        {
            string Update = "update Campaigns set CampaignName = @campaignName,NonProfitName=@NonProfitName, Hashtag=@hashtag,Description = @description where CampaignID = @campaignId";

            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.RunUpdateCampaign(Update, UpdateCampaign, campaign);
        }

        //Function that delete campaign 
        public void RemoveCampaign(System.Data.SqlClient.SqlCommand command, int? campaignId)
        {
            try
            {
                if (command == null && (campaignId == null))
                {
                    Log.LogError("command or camapignId are null");
                    return;
                }
                else
                {
                    command.Parameters.AddWithValue("@campaignId", campaignId);
                    int rows = command.ExecuteNonQuery();
                }
                
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
            
        }

        //Function i send to sqlquery
        public void DeleteCampaign(string campaignId)
        {
            string Delete = "delete from Campaigns where CampaignID = @campaignId";

            SqlQuery sqlQuery1 = new SqlQuery();
            sqlQuery1.RunDeleteCampaign(Delete, RemoveCampaign, int.Parse(campaignId));
        }

        //Function that loads one campaign from the  database with campaignID
        public object LoadOneCampiagn(System.Data.SqlClient.SqlCommand command, int? campaignId)
        {
            try
            {
                if (command == null && (campaignId == null))
                {
                    Log.LogError("command or campaign are null");
                    return null;
                }
                else
                {
                    command.Parameters.AddWithValue("@campaignId", campaignId);
                    Campaign campaign = new Campaign();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                campaign.CampaignID = reader.GetInt32(reader.GetOrdinal            ("CampaignID"));
                                campaign.CampaignName = reader.GetString               (reader.GetOrdinal("CampaignName"));
                                campaign.NonProfitName = reader.GetString(reader.GetOrdinal("NonProfitName"));
                                campaign.Hashtag = reader.GetString(reader.GetOrdinal              ("Hashtag"));
                                campaign.Description = reader.GetString(reader.GetOrdinal          ("Description"));


                            }
                        }
                    }
                    return campaign;
                }
               
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
            

        }

        public Campaign Load1Campaign(string campaignId)
        {
            string select = "select * from Campaigns where CampaignID=@campaignId";
            SqlQuery sqlQuery = new SqlQuery();
            object CampaignObj =sqlQuery.RunCampaign(select, LoadOneCampiagn, int.Parse(campaignId));
            Campaign campaign=null;
            if (CampaignObj is Campaign) { campaign=(Campaign)CampaignObj;}
            return campaign;
            
        }
    }
}

