using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public enum TypeLog
    {
        LogEvent,
        LogError,
        LogException,

    }


    public class Logger
    {
        static ILogger log;

        public static Queue<LogItem> itemsQueue;

        public Logger(string Provider)
        {
            itemsQueue = new Queue<LogItem>();
            Init(Provider);
            log.Init();
        }


        public void Init(string Provider)
        {
            switch (Provider)
            {
                case "LogDb":

                    log = new LogDB();
                    break;

                case "LogFile":
                    log = new LogFile();
                    break;

                case "LogConsole":
                    log = new LogConsole();
                    break;

                default:
                    log = new LogNone();
                    break;
            }
        }


        


        public static void LogEvent(string msg, DateTime date)
        {
            itemsQueue.Enqueue(new LogItem() { DateTime = date, Exception = null, Message = msg, TypeMsg = TypeLog.LogEvent });
        }
        public static void LogError(string msg, DateTime date)
        {
            itemsQueue.Enqueue(new LogItem() { DateTime = date, Exception = null, Message = msg, TypeMsg = TypeLog.LogError });
        }
        public static void LogException(string msg, Exception exce, DateTime date)
        {
            itemsQueue.Enqueue(new LogItem() { DateTime = date, Exception = exce, Message = msg, TypeMsg = TypeLog.LogException });
        }

    }
}
