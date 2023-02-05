using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SemesterBProject.Dal;

namespace Utilities
{
    public interface IDelegateFunction
    {
         void DeleteLog(object valueTime, SqlCommand command, string valueEvent, string valueError, string valueException);
         void InsertLog(object valueTime, SqlCommand command, string valueEvent, string valueError, string valueException);
    }
    public class LogDB : ILogger,IDelegateFunction
    {

        Task QueueTask = null;
        Task CheckTask = null;
        bool StopLoop { get; set; } = false;

        public LogItem Item = new LogItem();

        string AddProcedureName = "InsertLog";
        public void Init()
        {
            QueueTask = Task.Run(() =>
            {
                while (!StopLoop)
                {
                    if (Logger.itemsQueue.Count > 0)
                    {
                        LogItem item = Logger.itemsQueue.Dequeue();
                        if (item.TypeMsg == TypeLog.LogEvent)
                        {
                            LogEvent(item);
                        }
                        else if (item.TypeMsg == TypeLog.LogError)
                        {
                            LogError(item);
                        }
                        else if (item.TypeMsg == TypeLog.LogException)
                        {
                            LogException(item);
                        }


                    }

                    Thread.Sleep(100);
                }
            });

            CheckTask = Task.Run(() =>
            {
                while (!StopLoop)
                {
                    LogCheckHoseKeeping();

                    Thread.Sleep(1000 * 60 * 60);
                }
            });
        }

        public void LogEvent(LogItem log)
        {


            DataLayer.Data.RunAddCommand(AddProcedureName, InsertLog, log.DateTime, log.Message, null, null);


        }

        public void LogError(LogItem log)
        {

            DataLayer.Data.RunAddCommand(AddProcedureName, InsertLog, log.DateTime, null, log.Message, null);
        }

        public void LogException(LogItem log)
        {


            DataLayer.Data.RunAddCommand(AddProcedureName, InsertLog, log.DateTime, null, log.Message, log.Exception.Message);
        }

        public void InsertLog(object valueTime, SqlCommand command, string valueEvent, string valueError, string valueException)
        {

            try
            {
                if (command == null || valueTime == null) return;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@logEvent", valueEvent);
                command.Parameters.AddWithValue("@logError", valueError);
                command.Parameters.AddWithValue("@logException", valueException);
                command.Parameters.AddWithValue("@date", valueTime);
                int row = command.ExecuteNonQuery();



            }
            catch (Exception)
            {

                throw;
            }

        }

        public void LogCheckHoseKeeping()
        {
            string DeleteQuery = "checkExpairy";
            DataLayer.Data.RunAddCommand(DeleteQuery, DeleteLog, null, null, null, null);
        }

        public void DeleteLog(object valueTime, SqlCommand command, string valueEvent, string valueError, string valueException)
        {
            try
            {
                if (command == null) return;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                int rows = command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

                throw;
            }

        }
    }
}
