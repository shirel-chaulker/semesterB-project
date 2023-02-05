using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogFile : ILogger
    {

        string LogFileName { get; set; } = @"mylog.txt";
        Task QueueTask = null;
        Task CheckTask = null;
        bool StopLoop { get; set; } = false;

        public LogItem Item = new LogItem();



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

            using (StreamWriter sw = new StreamWriter(LogFileName, true))
            {
                sw.WriteLine($"log event: {log.DateTime}-{log.Message}");
            }
        }

        public void LogError(LogItem log)
        {

            using (StreamWriter sw = new StreamWriter(LogFileName, true))
            {
                sw.WriteLine($"log error: {log.DateTime}-{log.Message}");
            }
        }

        public void LogException(LogItem log)
        {

            using (StreamWriter sw = new StreamWriter(LogFileName, true))
            {
                sw.WriteLine($" Log Exception: {log.DateTime}-{log.Exception.Message} \n {log.Message}");
            }
        }
        public void CreateFile()
        {
            int CountNumberFile = 1;

            while (System.IO.File.Exists(LogFileName))
            {
                LogFileName = $@"mylog{CountNumberFile}.txt";

                CountNumberFile++;
            }
            using (FileStream file = new FileStream(LogFileName, FileMode.Create)) ;
        }
        public void LogCheckHoseKeeping()
        {
            if (!System.IO.File.Exists(LogFileName)) { using (FileStream file = new FileStream(LogFileName, FileMode.Create)) ; }

            else
            {
                var file = new FileInfo(LogFileName);
                if (file.Length >= 5242880)
                {
                    CreateFile();
                }
            }
        }
    }
}
