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

            var legazy = false;

            if (args.Length > 2)
            {
                if (args[0] == "github" || args[0] == "GitHubServer")
                {
                    if (args[1].Contains("--"))
                    {
                        legazy = true;
                    }
                }
            }

            if (args.Length > 3)
            {

                if (args[0] == "gitea" || args[0] == "GiteaServer")
                {
                    if (args[1].Contains("http") && args[2].Contains("--"))
                    {
                        legazy = true;
                    }
                }
            }



            if (!legazy)
            {
                Parser.Default.ParseVerbs<GithubCommands, GiteaCommands>(args)
                   .WithParsed<BaseCommand>(opts => opts.Execute());
            }
            else
            {
                string mode = "";
                string URL = "";
                string tag = "";
                string token = "";
                string owner = "";
                string repo = "";
                string filename = "";

                mode = args[0];

                var i = 0;

                if (mode == "gitea" || mode == "GiteaServer")
                {
                    i = 1;
                    URL = args[1];
                }

                tag = args[2 + i];
                token = args[4 + i];
                owner = args[6 + i];
                repo = args[8 + i];
                filename = args[10 + i];

                Console.WriteLine("Legazy!");
                Console.WriteLine($"Mode: {mode}, Tag: {tag}, Owner: {owner}, Repo: {repo}, Filename: {filename}");

                if (mode == "github" || mode == "GitHubServer")
                {

                    Console.WriteLine("WARNING: the github command is deprecated! use gitHub insted.");
                    var command = new GithubCommands.Upload();
                    command.Tag = tag;
                    command.Owner = owner;
                    command.Repo = repo;
                    command.FileName = filename;
                    command.Execute();
                }
            }
        }
    }
}
