using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;
using SemesterBProject.Model.Reports;

namespace SemesterBProject.Entities
{
    public class BusinessCompanies
    {
        public Dictionary<int, BusinessCompany> Init()
        {
            Data.Sql.BusinessComSql businessCom = new BusinessComSql();
            return businessCom.GetCompaniesFromDB();
            
        }

        public List<DeliveryTrack> GetDeliveries()
        {
            Data.Sql.BusinessComSql businessCom = new BusinessComSql();
            return businessCom.GetDeliveryFromDB();

        }


    }
}
