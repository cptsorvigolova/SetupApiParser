using SetupApiParser;
using System;
using System.IO;
using System.Linq;

namespace SetupApiParserTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var setupApiPath = @"D:\source\repos\usb_forensic\Sources&IoCs\setupapi\setupapi.dev.log";
            setupApiPath = @"D:\source\repos\usb_forensic\Sources&IoCs\setupapi\setupapi.dev.20210918_134609.log";
            var setupApi = File.ReadAllText(setupApiPath);
            var parser = new SetupApiParser.SetupApiParser();
            var result = parser.Parse(setupApi);
            var sections = result.BootSessions.SelectMany(x => x.Sections).OrderBy(x => x.ToString()).ToList();

            Console.ReadLine();
        }
    }
}
