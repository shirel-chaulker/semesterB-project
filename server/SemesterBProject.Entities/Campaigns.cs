using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Dal;
using SemesterBProject.Model;
using SemesterBProject.Data.Sql;
using System.Data.SqlClient;

namespace SemesterBProject.Entities
{
    public class Campaigns
    {
        public Campaigns() { }
        //Create a dictionary that will contain the campaigns data. The key of the dictionary is the campaign's ID and the value is the Product object
        Dictionary<int, Campaign> CampaignsDictionary = new Dictionary<int, Campaign>();

        

       
        public void addCampaign(Campaign campaign)
        {
           Data.Sql.CampaignSql campaign1 = new CampaignSql();
            campaign1.AddCampaignToTbl(campaign);
        }
        public Dictionary<int,Campaign> Init()
        {
            Data.Sql.CampaignSql campaign = new CampaignSql();
            return campaign.GetCampaignsFromDB();
        }


        public void UpdateCampaign(Campaign campaign)
        {
            Data.Sql.CampaignSql campaign2 = new CampaignSql();
            campaign2.EditCampaign(campaign);
        }

        public void Deletecampaign(string ID)
        {
            Data.Sql.CampaignSql campaign2 = new CampaignSql();
            campaign2.DeleteCampaign(ID);
        }

        public Campaign LoadCampaignById(string ID)
        {
            Data.Sql.CampaignSql campaign = new CampaignSql();
            return campaign.Load1Campaign(ID);
        }
    }
}
