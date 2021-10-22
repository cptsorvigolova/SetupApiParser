using System;
using System.IO;

namespace SetupApiParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var setupApiPath = @"D:\source\repos\usb_forensic\Sources&IoCs\setupapi\setupapi.dev.log";
            var setupApi = File.ReadAllText(setupApiPath);
            var parser = new SetupApiParser();
            var result = parser.Parse(setupApi);
            Console.WriteLine();
        }
    }
}
