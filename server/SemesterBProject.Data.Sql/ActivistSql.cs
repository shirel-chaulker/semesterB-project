using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemesterBProject.Dal;
using SemesterBProject.Model;

namespace SemesterBProject.Data.Sql
{
    public class ActivistSql
    {
        //function that take data from client and insert to db 
        // Function that insret data to user (social activist)
        public void InsertSocialActivist(SocialActivist activist, System.Data.SqlClient.SqlCommand command)
        {

            command.Parameters.AddWithValue("@firstName", activist.FirstName);
            command.Parameters.AddWithValue("@lastName", activist.LastName);
            command.Parameters.AddWithValue("@address", activist.Address);
            command.Parameters.AddWithValue("@email", activist.Email);
            command.Parameters.AddWithValue("@phoneNumber", activist.PhoneNumber);
            command.Parameters.AddWithValue("@twitterAcount", activist.TwitterAcount);
            int rows = command.ExecuteNonQuery();
        }

       
        public void AddActivistToTbl(SocialActivist activist)
        {
            string Insert = "insert into [dbo].[SocialActivist] values (@firstName,@lastName,@address,@email,@phoneNumber,@twitterAcount)";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.RunAddActivist(Insert, InsertSocialActivist, activist);
        }


        //create dictionary for activists
        Dictionary<int, SocialActivist> ActivistsDictionary = new Dictionary<int, SocialActivist>();

        //read from db and inset to dictionary
        public void AddActivistToDictionary(SqlDataReader reader)
        {

            //clear dictionary
            ActivistsDictionary.Clear();

            while (reader.Read())
            {
                SocialActivist activist = new SocialActivist();

                activist.ActivistId = reader.GetInt32(reader.GetOrdinal("ActivistId"));
                activist.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                activist.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                activist.Address= reader.GetString(reader.GetOrdinal("Address"));
                activist.Email = reader.GetString(reader.GetOrdinal("Email"));
                activist.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                activist.TwitterAcount = reader.GetString(reader.GetOrdinal("TwitterAcount"));


                //add the new business company to dictionary 
                ActivistsDictionary.Add(activist.ActivistId, activist);
            }

        }

        public Dictionary<int, SocialActivist> GetActivistFromDB()
        {
            string insert = "select * from [dbo].[SocialActivist]";
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.runCommand(insert, AddActivistToDictionary);
            return ActivistsDictionary;
        }

    }
}
