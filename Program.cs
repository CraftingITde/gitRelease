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
            Parser.Default.ParseArguments<githubOptions, giteaOptions>(args)
               .WithParsed<githubOptions>(opts => RunOptionsAndReturnExitCode(opts));
        }

        private static void RunOptionsAndReturnExitCode(githubOptions opts)
        {
            Console.WriteLine("Jej!");
            Console.WriteLine(opts.Verbose);
            Console.WriteLine(opts.Tag);
        }
    }
}
