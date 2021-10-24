using SetupApiParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupApiParser
{
    public class SetupApiParser
    {
        public DeviceInstallLog Parse(string setupApiDevLog)
        {
            var result = new DeviceInstallLog();
            setupApiDevLog = setupApiDevLog.Replace("\r", "");
            var lines = setupApiDevLog.Split('\n');
            var i = 0;
            var parameters = new Dictionary<string, string>();
            while (!lines[i].Contains(Constants.SetupApiStart))
            {
                i++;
            }
            while (!lines[i + 2].Contains(Constants.SetupApiBeginLog))
            {
                i++;
                var pair = lines[i].Split('=', 2).Select(x => x.Trim()).ToArray();
                parameters.Add(pair[0], pair[1]);
            }
            result.Parameters = parameters;
            result.BootSessions = new List<BootSession>();
            var bootSections = Constants.BootSessionRegex.Matches(setupApiDevLog).Select(x => x.Value);
            foreach (var boot in bootSections)
            {
                result.BootSessions.Add(ParseBootSession(boot));
            }

            return result;
        }

        private BootSession ParseBootSession(string bootSession)
        {
            var result = new BootSession();
            result.BootDate = DateTime.Parse(Constants.DateTimeRegex.Match(bootSession).Value);
            var parsed = Constants.SectionRegex.Matches(bootSession);
            var sections = parsed.Select(x => x.Value.Trim().Split('\n')).ToList();
            result.Sections = sections.Select(x => ParseSection(x)).ToList();
            return result;
        }

        private LogSection ParseSection(string[] section)
        {
            var result = new LogSection();
            result.Header = section[0].Substring(5);
            result.Start = DateTime.Parse(Constants.DateTimeRegex.Match(section[1]).Value);
            var last = section.Length - 1;
            result.End = DateTime.Parse(Constants.DateTimeRegex.Match(section[last - 1]).Value);
            result.ExitStatus = section[last].Substring(5);
            var process = section[2].Trim().Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
            result.Process = process[1];
            result.ProcessParams = process.Length > 2 ? process[2] : string.Empty;
            var entries = new List<LogEntry>();
            foreach (var e in section.Skip(3).Take(last - 3))
            {
                entries.Add(ParseEvent(e));
            }
            result.Entries = entries;
            return result;
        }

        private LogEntry ParseEvent(string entry)
        {
            var result = new LogEntry();
            var prefix = entry.Substring(0, 4);
            if (Mapping.EntryPrefixes.ContainsKey(prefix))
            {
                result.Prefix = Mapping.EntryPrefixes[prefix];
            }
            else
            {
                result.Prefix = EntryPrefix.Unknown;
            }
            var category = entry.Substring(5, 5);
            if (Mapping.EventCategories.ContainsKey(category))
            {
                result.Category = Mapping.EventCategories[category];
                result.Message = entry.Substring(10);
            }
            else
            {
                result.Category = EventCategory.Unknown;
                result.Message = entry.Substring(5);
            }
            return result;
        }
    }
}
