using System;
using System.Collections.Generic;
using SemesterBProject.Dal;
using SemesterBProject.Model;
using SemesterBProject.Data.Sql;
using System.Data.SqlClient;
using Utilities;

namespace SemesterBProject.Entities
{
    public class Campaigns: BaseEntity
    {

        public Campaigns (Logger log) : base (log)
        {
            
        }
       
        //Create a dictionary that will contain the campaigns data. The key of the dictionary is the campaign's ID and the value is the Product object
        Dictionary<int, Campaign> CampaignsDictionary = new Dictionary<int, Campaign>();

       
        public void addCampaign(Campaign campaign)
        {
           
                Log.LogEvent("Activates the function AddCampaignToTbl");
                Data.Sql.CampaignSql campaign1 = new CampaignSql(Log);
                campaign1.AddCampaignToTbl(campaign);
      
        }
        public Dictionary<int,Campaign> Init()
        {
            
                Log.LogEvent("Activates the function GetCampaignsFromDB");
                Data.Sql.CampaignSql campaign = new CampaignSql(Log);
                return campaign.GetCampaignsFromDB();
            
        }


        public void UpdateCampaign(Campaign campaign)
        {
            Log.LogEvent("Activates the function EditCampaign");
            Data.Sql.CampaignSql campaign2 = new CampaignSql(Log);
            campaign2.EditCampaign(campaign);
        }

        public void Deletecampaign(string ID)
        {
            Log.LogEvent("Activates the function DeleteCampaign");
            Data.Sql.CampaignSql campaign2 = new CampaignSql(Log);
            campaign2.DeleteCampaign(ID);
        }

        public Campaign LoadCampaignById(string ID)
        {
            Log.LogEvent("Activates the function Load1Campaign");
            Data.Sql.CampaignSql campaign = new CampaignSql(Log);
            return campaign.Load1Campaign(ID);
        }
    }
}
