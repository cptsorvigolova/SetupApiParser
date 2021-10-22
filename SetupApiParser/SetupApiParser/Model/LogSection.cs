using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupApiParser.Model
{
    public class LogSection
    {
        public string Header { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<LogEntry> Entries { get; set; }
        public string ExitStatus { get; set; }
    }
}
