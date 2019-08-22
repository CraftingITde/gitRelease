using System;
using System.Collections.Generic;
using CommandLine;
using gitRelease.Types;

namespace gitRelease
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
               .WithParsed(opts => RunOptionsAndReturnExitCode(opts));
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            Console.WriteLine("Jej!");
            Console.WriteLine(opts.Verbose);
        }
    }
}
