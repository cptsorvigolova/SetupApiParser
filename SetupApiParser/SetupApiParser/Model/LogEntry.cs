using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupApiParser.Model
{
    public class LogEntry
    {
        public string Prefix { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
    }
}
