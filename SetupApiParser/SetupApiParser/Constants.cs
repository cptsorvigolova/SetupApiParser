using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SetupApiParser
{
    public static class Constants
    {
        public static Regex DateTimeRegex = new Regex(@"\d{4}/\d\d/\d\d \d\d:\d\d:\d\d\.\d{2,3}");
        public static Regex SectionRegex = new Regex(@"\n(.+\n)+\n");
        public static Regex BootSessionHeaderRegex = new Regex($"\\[Boot Session: {DateTimeRegex}]");

        public const string SetupApiStart = "[Device Install Log]";
        public const string SetupApiBeginLog = "[BeginLog]";

        public static Dictionary<string, string> EntryPrefixes = new Dictionary<string, string>
        {
            {"!!! ","Error" },
            {"!   ","Warning" },
            {"    ","Info" }
        };

        public static Dictionary<string, string> EntryCategories = new Dictionary<string, string>
        {
            {"...: ", "Vendor-supplied operation" },
            {"bak: ", "Backup data" },
            {"cci: ", "Class installer or co-installer operation" },
            {"cpy: ", "Copy files" },
            {"dvi: ", "Device installation" },
            {"flq: ", "Manage file queues" },
            {"inf: ", "Manage INF files" },
            {"ndv: ", "New device wizard" },
            {"prp: ", "Manage device and driver properties" },
            {"reg: ", "Manage registry settings" },
            {"set: ", "General setup" },
            {"sig: ", "Verify digital signatures" },
            {"sto: ", "Manage the driver store" },
            {"ui : ", "Manage user interface dialog boxes" },
            {"ump: ", "User-mode PnP manager" }
        };


    }
}
