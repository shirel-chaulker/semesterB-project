using System;
using System.Collections.Generic;
using Utilities;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;
using SemesterBProject.Model.Reports;

namespace SemesterBProject.Entities
{
    public class BusinessCompanies: BaseEntity
    {
         
        public BusinessCompanies (Logger log) : base (log)
        {
            
        }

        public Dictionary<int, BusinessCompany> Init()
        {
                Log.LogEvent("Activates the function GetCompaniesFromDB");

                Data.Sql.BusinessComSql businessCom = new BusinessComSql(Log);
                return businessCom.GetCompaniesFromDB();
        }

        public List<DeliveryTrack> GetDeliveries()
        {
                Log.LogEvent("Activates the function GetDeliveryFromDB");

                Data.Sql.BusinessComSql businessCom = new BusinessComSql(Log);
                return businessCom.GetDeliveryFromDB();

        }


    }
}
