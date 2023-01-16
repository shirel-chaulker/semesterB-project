using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Dal;
using SemesterBProject.Model;
using SemesterBProject.Model.Reports;
using SemesterBProject.Model.Twitter;

namespace SemesterBProject.Data.Sql
{
    public class BusinessComSql
    {
        //create dictionary for businesscompany tbl
        Dictionary<int, BusinessCompany> CompaniesDictionary = new Dictionary<int, BusinessCompany>();

        //Function that read from db and add one by one to the dictionary
        public void AddBusinessComToDictionary(SqlDataReader reader)
        {

            //clear dictionary
            CompaniesDictionary.Clear();

            while (reader.Read())
            {
                BusinessCompany businessCompany = new BusinessCompany();

                businessCompany.BusinessID = reader.GetInt32(reader.GetOrdinal("BusinessID"));
                businessCompany.CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"));
                businessCompany.URL = reader.GetString(reader.GetOrdinal("URL"));
                businessCompany.Email = reader.GetString(reader.GetOrdinal("Email"));
                businessCompany.PhoneNumber= reader.GetString(reader.GetOrdinal("PhoneNumber"));


                //add the new business company to dictionary 
                CompaniesDictionary.Add(businessCompany.BusinessID, businessCompany);
            }

        }

        public Dictionary<int, BusinessCompany> GetCompaniesFromDB()
        {
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

            //clear List
            DeliveryList.Clear();

            while (reader.Read())
            {
               DeliveryTrack delivery = new DeliveryTrack();

                delivery.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                delivery.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                delivery.Address = reader.GetString(reader.GetOrdinal("Address"));
                delivery.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));



                //add to the list
                DeliveryList.Add(delivery);
            }

        }

        public List<DeliveryTrack> GetDeliveryFromDB()
        {
            string insert = "declare @donateCompany nvarchar(max)\r\nselect [dbo].[Shopping].[ProductName],[dbo].[Shopping].[FullName],[dbo].[Shopping].[Address],\r\n[dbo].[Shopping].[PhoneNumber] from [dbo].[Shopping] inner join [dbo].[Products] on [dbo].[Shopping].[ProductName]=[dbo].[Products].[ProductName]\r\nwhere [dbo].[Products].[donate_company]=@donateCompany";

            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.runCommand(insert, AddDeliveryToList);
            return DeliveryList;
        }

    }
}
