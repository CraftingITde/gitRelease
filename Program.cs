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
               .WithParsed<githubOptions>(opts => RunGithub(opts))
               .WithParsed<giteaOptions>(opts => RunGitea(opts));
        }

        private static void RunGithub(githubOptions opts)
        {
            Console.WriteLine("Github!");
            Console.WriteLine(opts.Verbose);
            Console.WriteLine(opts.Tag);
        }

        private static void RunGitea(giteaOptions opts)
        {
            Console.WriteLine("Gitea!");
            Console.WriteLine(opts.Verbose);
            Console.WriteLine(opts.Tag);
        }
    }
}
