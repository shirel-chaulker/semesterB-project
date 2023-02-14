using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NLog.Fluent;
using SemesterBProject.Dal;
using SemesterBProject.Model;
using SemesterBProject.Model.Reports;
using SemesterBProject.Model.Twitter;
using Utilities;

namespace SemesterBProject.Data.Sql
{
    public class BusinessComSql: BaseDataSql
    {
        public BusinessComSql(Logger log) : base(log)
        {

        }
        //create dictionary for businesscompany tbl
        Dictionary<int, BusinessCompany> CompaniesDictionary = new Dictionary<int, BusinessCompany>();

        //Function that read from db and add one by one to the dictionary
        public void AddBusinessComToDictionary(SqlDataReader reader)
        {
            Log.LogEvent("clear dictionary");
            //clear dictionary
            CompaniesDictionary.Clear();

            try
            {
                Log.LogEvent("start to read business company table from db");
                while (reader.Read())
                {
                    BusinessCompany businessCompany = new BusinessCompany();

                    businessCompany.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                    businessCompany.CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"));
                    businessCompany.URL = reader.GetString(reader.GetOrdinal("URL"));
                    businessCompany.Email = reader.GetString(reader.GetOrdinal("Email"));
                    businessCompany.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                    Log.LogEvent("add the new business company to dictionary");
                    //add the new business company to dictionary 
                    CompaniesDictionary.Add(businessCompany.BusinessID, businessCompany);
                }
            }
            catch (Exception ex)
            {
                Log.LogException("cannot read data from db", ex);
                throw;
                
            }
            

        }

        public Dictionary<int, BusinessCompany> GetCompaniesFromDB()
        {
            Log.LogEvent("return dictionary to azure");
            string insert = "select * from BusinessCompany";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.runCommand(insert, AddBusinessComToDictionary);
            return CompaniesDictionary;
        }



        //// report company 
        List<DeliveryTrack> DeliveryList = new List<DeliveryTrack>();

      
        
        //read from db and inset to list
        public void AddDeliveryToList(SqlDataReader reader)
        {
            Log.LogEvent("read from db and inset to list");
            //clear List
            DeliveryList.Clear();

            try
            {
                while (reader.Read())
                {
                    DeliveryTrack delivery = new DeliveryTrack();

                    delivery.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                    delivery.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    delivery.Address = reader.GetString(reader.GetOrdinal("Address"));
                    delivery.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                  
                    //add to the list
                    DeliveryList.Add(delivery);
                    Log.LogEvent("the table was read and add to the list");
                }
            }
            catch (Exception ex)
            {
                Log.LogException("cannot read data-delivery from db",ex);
                throw;
            }
           

        }

        public List<DeliveryTrack> GetDeliveryFromDB()
        {
            try
            {
                string insert = "declare @donateCompany nvarchar(max)\r\nselect [dbo].[Shopping].[ProductName],[dbo].[Shopping].[FullName],[dbo].[Shopping].[Address],\r\n[dbo].[Shopping].[PhoneNumber] from [dbo].[Shopping] inner join [dbo].[Products] on [dbo].[Shopping].[ProductName]=[dbo].[Products].[ProductName]\r\nwhere [dbo].[Products].[donate_company]=@donateCompany";

                SqlQuery sqlQuery = new SqlQuery();
                sqlQuery.runCommand(insert, AddDeliveryToList);
                return DeliveryList;
            }
            catch (Exception ex)
            {
                Log.LogException("failed insert data to dal", ex);
                throw;
            }
            
        }

    }
}
