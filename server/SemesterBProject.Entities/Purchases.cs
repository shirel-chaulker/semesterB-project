using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Model;

namespace SemesterBProject.Entities
{
   public class Purchases
   {
        public void PostPurchase(Purchase UserResponse) //post
        {
            Data.Sql.purchaseSql purchase = new Data.Sql.purchaseSql();
            purchase.AddPurchaseToTbl(UserResponse);
        }
   }
}
