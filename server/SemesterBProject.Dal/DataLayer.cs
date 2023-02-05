using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterBProject.Dal
{
    public class DataLayer: DbContext
    {
        static string connectionString = @"data source=localhost\SQLEXPRESS; initial catalog=LogTable;persist security info=True; Integrated Security=SSPI";
        SqlConnection connection;

        private static DataLayer data;
        public DataLayer() : base(connectionString)
        {
            connection = new SqlConnection(connectionString);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataLayer>());
        }

        public static DataLayer Data
        {
            get
            {
                if (data == null) data = new DataLayer();
                return data;
            }
        }

        public DbSet<LogTable> LogTable { get; set; }

        public bool Connect()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {

                return false;
            }
        }
        public bool CloseConnect()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public delegate void Data_delegate(object valueTime, SqlCommand command, string valueEvent, string valueError, string valueException);

        public void RunAddCommand(string sqlQuerey, Data_delegate func, object valueTime, string valueEvent, string valueError, string valueException)
        {

            if (!Connect()) return;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                func(valueTime, command, valueEvent, valueError, valueException);

            }
            if (!CloseConnect()) return;

        }

    }
}
