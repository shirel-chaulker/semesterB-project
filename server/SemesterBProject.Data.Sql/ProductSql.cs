using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Dal;
using SemesterBProject.Model;
using Utilities;

namespace SemesterBProject.Data.Sql
{
    public class ProductSql: BaseDataSql
    {
        public ProductSql(Logger log) : base(log)
        {

        }
        //create dictionary
        Dictionary<int, Product> ProductsDictionary = new Dictionary<int, Product>();

        //read from db and inset to dictionary
        public void AddProductToDictionary(SqlDataReader reader)
        {
            try
            {
                Log.LogEvent("read from db and inset to dictionary");
                //clear dictionary
                ProductsDictionary.Clear();

                while (reader.Read())
                {
                    Product product = new Product();

                    product.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                    product.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                    product.Product_Value = reader.GetDecimal(reader.GetOrdinal("Product_Value"));
                    product.donate_company = reader.GetString(reader.GetOrdinal("donate_company"));
                    product.NonProfitName = reader.GetString(reader.GetOrdinal("NonProfitName"));
                    product.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));

                    //add the new product to dictionary 
                    Log.LogEvent("add the new product to dictionary");
                    ProductsDictionary.Add(product.ProductID, product);
                }
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
           
            
        }

        public Dictionary<int, Product> GetProductsFromDB()
        {
            string insert = "select * from Products";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.runCommand(insert, AddProductToDictionary);
            return ProductsDictionary;
        }

        //function that take data from client and insert to db 
        // Function that insret data to product
        public void InsertProduct(Product product, System.Data.SqlClient.SqlCommand command)
        {
            Log.LogEvent("add data from client to db");
            try
            {
                command.Parameters.AddWithValue("@productName", product.ProductName);
                command.Parameters.AddWithValue("@ProductValue", product.Product_Value);
                command.Parameters.AddWithValue("@donateCompany", product.donate_company);
                command.Parameters.AddWithValue("@NonProfitName", product.NonProfitName);
                command.Parameters.AddWithValue("@campaignName", product.CampaignName);
                int rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
            
        }


        public void AddProductToTbl(Product product)
        {
            string Insert = "insert into [dbo].[Products] values (@productName,@ProductValue,@donateCompany,\r\n@nonProfitName, @campaignName)";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.RunAddProduct(Insert, InsertProduct,product);
        }


        //Function that loads one product from the  database with productID
        public object LoadOneProduct(System.Data.SqlClient.SqlCommand command, int? ProductId)
        {
            Log.LogEvent("get one product with all data");
            try
            {
                if (command == null && (ProductId == null))
                {
                    Log.LogError("command or productId are null");
                    return null;
                }
                else
                {
                    command.Parameters.AddWithValue("@productID", ProductId);
                    Product product = new Product();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                product.ProductID = reader.GetInt32(reader.GetOrdinal("ProductID"));
                                product.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                                product.Product_Value = reader.GetDecimal(reader.GetOrdinal("Product_Value"));
                                product.donate_company = reader.GetString(reader.GetOrdinal("donate_company"));
                                product.NonProfitName = reader.GetString(reader.GetOrdinal("NonProfitName"));
                                product.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));

                            }
                        }
                    }
                    return product;
                }
                   
            }
            catch (Exception ex)
            {
                Log.LogException("An exception occurred:", ex);
                throw;
            }
            

        }

        public Product Load1Product(string ProductId)
        {
            Log.LogEvent("insert data to dal function");
            string select = "select * from [dbo].[Products] where [ProductID]=@productID";
            SqlQuery sqlQuery = new SqlQuery();
            object ProductObj = sqlQuery.RunProduct(select, LoadOneProduct, int.Parse(ProductId));
            Product product = null;
            if (ProductObj is Product) { product = (Product)ProductObj; }
            return product;

        }
    }
}
