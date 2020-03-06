using System;
using System.Collections.Generic;
using CommandLine;
using gitRelease.Commands;

namespace gitRelease
{
    class Program
    {
        static void Main(string[] args)
        {

            Parser.Default.ParseVerbs<GithubCommands, GiteaCommands>(args)
                .WithParsed<BaseCommand>(opts => opts.Execute());
         
        }
    }
}
