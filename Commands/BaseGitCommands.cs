using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace gitRelease.Commands
{
    public class BaseGitCommands : BaseCommand
    {
        [Option('t', "tag", Required = true, HelpText = "The tag vor the Release")]
        public string Tag { get; set; }

        [Option('a', "ApiToken", Required = true, HelpText = "The the API Token")]
        public string Token { get; set; }

        [Option('o', "owner", Required = true, HelpText = "The reposetory Owner")]
        public string Owner { get; set; }

        [Option('r', "repo", Required = true, HelpText = "The reposetory Name")]
        public string Repo { get; set; }


        public override void Execute()
        {

        }
    }
}
