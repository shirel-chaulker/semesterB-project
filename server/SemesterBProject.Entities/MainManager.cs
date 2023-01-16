using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Model;

namespace SemesterBProject.Entities
{
    public class MainManager
    {

        private MainManager() { }

        private static readonly MainManager _instance = new MainManager();
        public static MainManager Instance { get { return _instance; } }


        //create show for the classes
        public Campaigns campaigns = new Campaigns();

        public BusinessCompanies BusinessComp = new BusinessCompanies();

        public Products products = new Products();

        public SocialActivists activists = new SocialActivists();

        public Twitters twitters = new Twitters();

        public NonProfitOrgs NonProfit = new NonProfitOrgs();

        public Purchases Purchases = new Purchases();
       
    }
}
