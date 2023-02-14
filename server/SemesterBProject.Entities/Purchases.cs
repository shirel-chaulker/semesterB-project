using Utilities;
using SemesterBProject.Model;

namespace SemesterBProject.Entities
{
   public class Purchases: BaseEntity
    {
        
        public Purchases (Logger log):base(log)
        {
            
        }
        public void PostPurchase(Purchase UserResponse) //post
        {
            Data.Sql.purchaseSql purchase = new Data.Sql.purchaseSql(Log);
            purchase.AddPurchaseToTbl(UserResponse);
        }
   }
}
