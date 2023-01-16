using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Model;
using SemesterBProject.Dal;

namespace SemesterBProject.Data.Sql
{
    public class CampaignSql
    {
        //create dictionary
        Dictionary<int, Campaign> CampaignsDictionary = new Dictionary<int, Campaign>();
        // Function that insret data to campaign
        public void InsertCampaign(Campaign campaign, System.Data.SqlClient.SqlCommand command)
        {
           
            command.Parameters.AddWithValue("@campaignName", campaign.CampaignName);
            command.Parameters.AddWithValue("@NonProfitName", campaign.NonProfitName);
            command.Parameters.AddWithValue("@hashtag", campaign.Hashtag);
            command.Parameters.AddWithValue("@description", campaign.Description);
            int rows = command.ExecuteNonQuery();
        }

        public void AddCampaignToTbl(Campaign campaign)
        {
            string Insert = " insert into Campaigns values (@campaignName,@NonProfitName,@hashtag,@description)";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.RunAddCampaign(Insert, InsertCampaign, campaign);
        }


        public void AddCampignToDictionary(SqlDataReader reader)
        {

            //clear dictionary
            CampaignsDictionary.Clear();

            while (reader.Read())
            {
                Campaign campaign = new Campaign();

                campaign.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                campaign.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                campaign.NonProfitName = reader.GetString(reader.GetOrdinal("NonProfitName"));
                campaign.Description = reader.GetString(reader.GetOrdinal("Description"));
                campaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                

                //add the new campaign to dictionary 
                CampaignsDictionary.Add(campaign.CampaignID, campaign);
            }

        }
        public Dictionary<int, Campaign> GetCampaignsFromDB()
        {
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
                if (command == null && (campaign == null)) return;
                {
                    command.Parameters.AddWithValue("@campaignName", campaign.CampaignName);
                    command.Parameters.AddWithValue("@NonProfitName", campaign.NonProfitName);
                    command.Parameters.AddWithValue("@hashtag", campaign.Hashtag);
                    command.Parameters.AddWithValue("@description", campaign.Description);
                    int rows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

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
            if (command == null && (campaignId == null)) return;
            command.Parameters.AddWithValue("@campaignId", campaignId);
            int rows = command.ExecuteNonQuery();
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
            if (command == null && (campaignId == null)) return null;
            command.Parameters.AddWithValue("@campaignId", campaignId);
            Campaign campaign = new Campaign();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        campaign.CampaignID = reader.GetInt32(reader.GetOrdinal("CampaignID"));
                        campaign.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                        campaign.NonProfitName = reader.GetString(reader.GetOrdinal("NonProfitName"));
                        campaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag")); 
                        campaign.Description = reader.GetString(reader.GetOrdinal("Description"));


                    }
                }
            }
            return campaign;

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

