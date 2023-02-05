using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogConsole : ILogger
    {
        Task QueueTask = null;
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

        }

        public void LogEvent(LogItem log)
        {
            Console.WriteLine($"Log event:{log.Message}");

        }

        public void LogError(LogItem log)
        {
            Console.WriteLine($"Log Error: {log.Message}");
        }

        public void LogException(LogItem log)
        {
            Console.WriteLine($"the Log Exception is: {log.Message}\n {log.Exception.Message}");
        }
        public void LogCheckHoseKeeping()
        {

        }
    }
}
