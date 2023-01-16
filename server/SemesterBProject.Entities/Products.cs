using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;

namespace SemesterBProject.Entities
{
    public class Products
    {
        public Dictionary<int, Product> Init()
        {
            Data.Sql.ProductSql product = new ProductSql();
            return product.GetProductsFromDB();
        }

        public void addProduct(Product product)
        {
            Data.Sql.ProductSql productSql = new ProductSql();
            productSql.AddProductToTbl(product);
        }

        public Product LoadpRroductById(string ID)
        {
            Data.Sql.ProductSql proSql = new ProductSql();
            return proSql.Load1Product(ID);
        }

    }
}
