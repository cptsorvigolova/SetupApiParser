using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupApiParser.Model
{
    public class DeviceInstallLog
    {
        public Dictionary<string, string> Parameters { get; set; }
        public List<BootSession> BootSessions { get; set; }
    }
}
