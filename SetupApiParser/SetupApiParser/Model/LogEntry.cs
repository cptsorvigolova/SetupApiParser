using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupApiParser.Model
{
    public class LogEntry
    {
        public EntryPrefix Prefix { get; set; }
        public EventCategory Category { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return $"{Prefix} {Category} {Message}";
        }
    }
}
