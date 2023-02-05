using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class UtilitiesProject
    {
    }
    public interface ILogger
    {
        void Init();
        void LogEvent(LogItem log);
        void LogError(LogItem log);
        void LogException(LogItem log);
        void LogCheckHoseKeeping();
    }
}
