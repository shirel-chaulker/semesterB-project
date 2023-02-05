using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogItem
    {
        public DateTime DateTime { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public TypeLog TypeMsg { get; set; }

    }
}
