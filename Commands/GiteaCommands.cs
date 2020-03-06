using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace gitRelease.Commands
{
    [Verb("gitea", HelpText = "Record changes to the repository.")]

    [ChildVerbs(typeof(Update), typeof(Upload), typeof(IsDraft), typeof(IsPrerelease))]
    class GiteaCommands : BaseGitCommands
    {

        [Option('s', "server", Required = true, HelpText = "the Server Adress")]
        public string Server { get; set; }

        [Option('h', HelpText = "use Http. Default is https")]
        public bool Http { get; set; } = false;

        [Option('p', HelpText = "Port to use. Default is 443")]
        public int Port { get; set; } = 443;

        [Verb("upload", HelpText = "Upload an asset")]
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

        [Verb("update", HelpText = "updating a Tag")]
        public class Update : GiteaCommands
        {

            [Option('n', "Name", Required = false, HelpText = "The name for the Release")]
            public string Name { get; set; }

            [Option("Prerelease", Required = false, HelpText = "Prerelease?")]
            public bool Prerelease { get; set; }

            [Option('d', "Draft", Required = false, HelpText = "Draftrelease?")]
            public bool DraftRelease { get; set; }

            [Option('b', "Body", Required = true, HelpText = "The Release Body")]
            public string Body { get; set; }

            public override void Execute()
            {

                var client = new Gitea.API.v1.Client(Token, Server, Port, !Http);
                client.Repository.Release.updateRelease(Owner, Repo, Tag, Body, Name, Prerelease, DraftRelease);

            }
        }
        [Verb("isDraft", HelpText = "is the release a draft?")]
        public class IsDraft : GiteaCommands
        {

            public override void Execute()
            {

                var client = new Gitea.API.v1.Client(Token, Server, Port, !Http);
                var list = client.Repository.Release.Get(Owner, Repo);
                var release = list.Find(r => r.Tag_name == this.Tag);
                Console.WriteLine(release.Draft ? "1" : "0");
            }
        }
        [Verb("isPrerelease", HelpText = "is the release a Prerelease?")]
        public class IsPrerelease : GiteaCommands
        {

            public override void Execute()
            {

                var client = new Gitea.API.v1.Client(Token, Server, Port, !Http);
                var list = client.Repository.Release.Get(Owner, Repo);
                var release = list.Find(r => r.Tag_name == this.Tag);
                
                Console.WriteLine(release.Prerelease ? "1" : "0");
            }
        }

    }
}