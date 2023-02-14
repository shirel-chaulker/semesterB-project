using System;
using System.Collections.Generic;
using Utilities;
using SemesterBProject.Data.Sql;
using SemesterBProject.Model;

namespace SemesterBProject.Entities
{
    public class Products: BaseEntity
    {

        
        public Products(Logger log) : base(log)
        {
            
        }

        public Dictionary<int, Product> Init()
        {
            Data.Sql.ProductSql product = new ProductSql(Log);
            return product.GetProductsFromDB();
        }

        public void addProduct(Product product)
        {
            Data.Sql.ProductSql productSql = new ProductSql(Log);
            productSql.AddProductToTbl(product);
        }

        public Product LoadpRroductById(string ID)
        {
            Data.Sql.ProductSql proSql = new ProductSql(Log);
            return proSql.Load1Product(ID);
        }

    }
}
