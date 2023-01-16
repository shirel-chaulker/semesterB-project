using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;

namespace SemesterBProject.Entities
{
    public class SocialActivists
    {
        //insert data drom client
        public void addActivist(SocialActivist activist)
        {
            Data.Sql.ActivistSql activistSql = new Data.Sql.ActivistSql();
            activistSql.AddActivistToTbl(activist);
        }

        public Dictionary<int, SocialActivist> Init()
        {
            Data.Sql.ActivistSql activist =new ActivistSql();
            return activist.GetActivistFromDB();
        }
    }
}
