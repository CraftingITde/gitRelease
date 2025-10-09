using CommandLine;
using gitRelease.Types;
using Octokit;
using System;
using System.Collections.Generic;
using System.Text;

namespace gitRelease.Commands
{
    [Verb("github", HelpText = "Add file contents to the index.")]
    [ChildVerbs(typeof(Create), typeof(Update), typeof(Upload), typeof(UpdateBody) , typeof(IsDraft), typeof(IsPrerelease))]
    public class GithubCommands : BaseGitCommands
    {
        [Option('s', "server", Required = true, HelpText = "the Server Adress")]
        public string Server { get; set; } = "github.com";
        
        [Verb("create", HelpText = "Creates a new Release")]
        public class Create : GithubCommands
        {

            [Option('n', "Name", Required = false, HelpText = "The name for the Release")]
             public string Name { get; set; }

            [Option('p', "Prerelease", Required = false, HelpText = "Prerelease?")]
            public bool Prerelease { get; set; }

            [Option('d', "Draft", Required = false, HelpText = "Draftrelease?")]
            public bool DraftRelease { get; set; }


            [Option('b', "Body", Required = true, HelpText = "The Release Body")]
            public string Body { get; set; }

            public override void Execute()
            {
                var github = new GithubApi(Token, Owner, Repo, Server);

                github.CreateRelease(Tag, Body, Name, Prerelease, DraftRelease);
            }

        }
        [Verb("update", HelpText = "Updates a present Release")]
        public class Update : GithubCommands
        {

            [Option('n', "Name", Required = false, HelpText = "The name for the Release")]
            public string Name { get; set; }

            [Option('p', "Prerelease", Required = false, HelpText = "Prerelease?")]
            public bool Prerelease { get; set; }

            [Option('d', "Draft", Required = false, HelpText = "Draftrelease?")]
            public bool DraftRelease { get; set; }

            [Option('b', "Body", Required = true, HelpText = "The Release Body")]
            public string Body { get; set; }

            public override void Execute()
            {
                var github = new GithubApi(Token, Owner, Repo, Server);

                github.UpdateRelease(Tag, Body, Name, Prerelease, DraftRelease);
            }
        }

        [Verb("updateBody", HelpText = "Updates a present Release")]
        public class UpdateBody : GithubCommands
        {

            [Option('l', "Lines", Required = false, HelpText = "Lines to add")]
            public string Lines { get; set; }

            [Option('b', "Body", Required = false, HelpText = "The Release Body")]
            public string Body { get; set; }

            public override void Execute()
            {
                var github = new GithubApi(Token, Owner, Repo, Server);

                github.UpdateReleaseBody(Tag, Lines, Body);
            }
        }


        [Verb("upload", HelpText = "Upload an asset")]
        public class Upload : GithubCommands
        {

            [Option("filename", Required = false, HelpText = "File to Upload")]
            public string FileName { get; set; }

            public override void Execute()
            {

                var github = new GithubApi(Token, Owner, Repo, Server);

                github.UploadAsset(Tag, FileName);
            }
        }

        [Verb("isDraft", HelpText = "is the release a draft?")]
        public class IsDraft : GithubCommands
        {

            public override void Execute()
            {
                var github = new GithubApi(Token, Owner, Repo, Server);


                Release release = github.GetRelease(Tag); ;

                Console.WriteLine(release.Draft ? "1" : "0"); 

            }
        }
        [Verb("isPrerelease", HelpText = "is the release a Prerelease?")]
        public class IsPrerelease : GithubCommands
        {
            public override void Execute()
            {
                var github = new GithubApi(Token, Owner, Repo, Server);
                Release release = github.GetRelease(Tag); ;
                Console.WriteLine(release.Prerelease ? "1" : "0");
            }
        }
    }
}