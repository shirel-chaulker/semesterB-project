using System;
using System.Collections.Generic;
using Utilities;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;


namespace SemesterBProject.Entities
{
    public class NonProfitOrgs: BaseEntity
    {
       
        public NonProfitOrgs (Logger log): base(log)
        {
            
        }
        public void PostNonProfit(NonProfitOrg UserResponse) //post
        {
            Data.Sql.NonProfitSql profitSql = new Data.Sql.NonProfitSql(Log);
            profitSql.AddOrgToTbl(UserResponse);
        }

        public Dictionary<int, NonProfitOrg> Init() // get
        {
            Data.Sql.NonProfitSql nonProfit = new Data.Sql.NonProfitSql(Log);
            return nonProfit.GetOrgFromDB();
        }

        //get by id
        public NonProfitOrg LoadOrgById(string ID)
        {
            Data.Sql.NonProfitSql profitSql = new NonProfitSql(Log);
            return profitSql.Load1Org(ID);
        }
    }
}
