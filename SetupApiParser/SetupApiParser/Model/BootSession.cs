using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupApiParser.Model
{
    public class BootSession
    {
        public DateTime BootDate { get; set; }
        public List<LogSection> Sections { get; set; }
    }
}
