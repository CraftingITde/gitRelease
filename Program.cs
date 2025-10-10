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
            var parser = new Parser(config => config.IgnoreUnknownArguments = true);
            parser.ParseVerbs<GithubCommands, GiteaCommands, VersionCommands>(args)
                .WithParsed<BaseCommand>(opts => opts.Execute());
        }
    }
}
