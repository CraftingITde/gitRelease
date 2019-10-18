using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace gitRelease.Commands
{
    [Verb("gittea", HelpText = "Record changes to the repository.")]
    class GiteaCommands : BaseGitCommands
    {

        [Option('s', "server", Required = true, HelpText = "the Server Adress")]
        public string Server { get; set; }

        [Option('h', HelpText = "use Http. Default is https")]
        public bool Http { get; set; } = false;
        
        [Option('p', HelpText = "Port to use. Default is 443")]
        public int Port { get; set; } = 443;

        [Verb("upload", HelpText = "Upload a asset")]
        public class Upload : GiteaCommands
        {

            [Option("filename", Required = false, HelpText = "File to Upload")]
            public string FileName { get; set; }

            public override void Execute()
            {
            
                var client = new Gitea.API.v1.Client(Token, Server, Port, !Http);

                client.Repository.Release.addAttatchment(Owner, Repo, Tag, FileName);

            }
        }
    }
}