using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using SemesterBProject.Model;
using SocialCommunication;

namespace SemesterBProject.Entities
{
    public class MainManager
    {

        Logger logger;
        private MainManager() { Init(); }

        private static readonly MainManager _instance = new MainManager();
        public static MainManager Instance { get { return _instance; } }


        //create show for the classes
        public void Init()
        {
            logger = new Logger("LogFile");
            campaigns = new Campaigns(logger);
            BusinessComp = new BusinessCompanies(logger);
            products = new Products(logger);
            activists = new SocialActivists(logger);
            twitters = new Twitters(logger);
            NonProfit = new NonProfitOrgs(logger);
            Purchases = new Purchases(logger);
            Tweets = new GetTwitter(logger);
        }
        public Campaigns campaigns;

        public BusinessCompanies BusinessComp;

        public Products products;

        public SocialActivists activists;

        public Twitters twitters;

        public NonProfitOrgs NonProfit;

        public Purchases Purchases;

        public GetTwitter Tweets;
       
    }
}
