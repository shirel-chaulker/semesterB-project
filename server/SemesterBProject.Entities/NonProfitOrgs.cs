using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;

namespace SemesterBProject.Entities
{
    public class NonProfitOrgs
    {
        public void PostNonProfit(NonProfitOrg UserResponse) //post
        {
            Data.Sql.NonProfitSql profitSql = new Data.Sql.NonProfitSql();
            profitSql.AddOrgToTbl(UserResponse);
        }

        public Dictionary<int, NonProfitOrg> Init() // get
        {
            Data.Sql.NonProfitSql nonProfit = new Data.Sql.NonProfitSql();
            return nonProfit.GetOrgFromDB();
        }

        //get by id
        public NonProfitOrg LoadOrgById(string ID)
        {
            Data.Sql.NonProfitSql profitSql = new NonProfitSql();
            return profitSql.Load1Org(ID);
        }
    }
}
