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
            setupApiDevLog = setupApiDevLog.Replace("\r","");
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
            while (i < lines.Length - 1)
            {
                i++;
                if (Constants.BootSessionHeaderRegex.IsMatch(lines[i]))
                {
                    var startLine = i - 1;
                    while (i < lines.Length - 1 && !Constants.BootSessionHeaderRegex.IsMatch(lines[i + 1]))
                    {
                        i++;
                    }
                    var toTake = i - 1 - startLine;
                    result.BootSessions.Add(ParseBootSession(string.Join("\n", lines.Skip(startLine).Take(toTake))));
                }
            }
            return result;
        }

        private BootSession ParseBootSession(string bootSession)
        {
            var result = new BootSession();
            result.BootDate = DateTime.Parse(Constants.DateTimeRegex.Match(bootSession).Value);
            var sections = Constants.SectionRegex.Matches(bootSession).Skip(1).Select(x => x.Value.Split('\n')).ToList();
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
            var entries = new List<LogEntry>();
            foreach (var e in section.Skip(2).Take(last - 3))
            {
                entries.Add(ParseEntry(e));
            }
            result.Entries = entries;
            return result;
        }

        private LogEntry ParseEntry(string entry)
        {
            var result = new LogEntry();
            var prefix = entry.Substring(0, 4);
            if (Constants.EntryPrefixes.ContainsKey(prefix))
            {
                result.Prefix = Constants.EntryPrefixes[prefix];
            }
            else
            {
                result.Prefix = prefix;
            }
            var category = entry.Substring(4, 5);
            if (Constants.EntryCategories.ContainsKey(category))
            {
                result.Category = Constants.EntryCategories[category];
                result.Message = entry.Substring(9);
            }
            else
            {
                result.Message = entry.Substring(4);
            }
            return result;
        }
    }
}
