using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace SemesterBProject.Data.Sql
{
    public class BaseDataSql
    {
        public Logger Log { get; set; }
        public BaseDataSql(Logger log)
        {
            Log = log;
        }
    }
}
