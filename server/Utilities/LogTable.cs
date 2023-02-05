using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Utilities
{
    public class LogTable
    {
        [Key]
        public int Id { get; set; }

        public string LogEvent { get; set; }
        public string LogError { get; set; }
        public string LogException { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
    }
}
