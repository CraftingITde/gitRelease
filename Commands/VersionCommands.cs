using CommandLine;
using gitRelease.Types;
using LibGit2Sharp;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace gitRelease.Commands
{

    [Verb("tag", HelpText = "Handel's semantic versioning.")]
    [ChildVerbs(typeof(Next), typeof(Generate))]
    public class VersionCommands : BaseCommand
    {
        protected Versions GetVersions(Repository repo)
        {
            Versions versions = new Versions(repo.Branches.First(b => b.IsCurrentRepositoryHead).FriendlyName);

            foreach (Branch b in repo.Branches.Where(b => b.IsRemote && b.RemoteName == RemoteName))
            {
                if (versions.addBranch(b.FriendlyName) && Verbose)
                {
                    Console.WriteLine($"{b.FriendlyName}");
                }
            }

            foreach (Tag t in repo.Tags)
            {
                if (versions.addTag(t.FriendlyName) && Verbose)
                {
                    Console.WriteLine($"{t.FriendlyName}");
                }
            }

            return versions;
        }

        public override void Execute()
        {

        }

        [Option('r', "Remote", HelpText = "Name of the remote")]
        public string RemoteName { get; set; } = "origin";

        [Option('p', "Path", HelpText = "Name of the remote")]
        public string RepositoryPath { get; set; } = "./";

        [Option('e', "Explicit", HelpText = "Only generate tag from commitmessage keywords")]
        public bool Explicit { get; set; } = false;

        [Verb("next", HelpText = "Gets the next available semantic version number.")]
        public class Next : VersionCommands
        {
            public override void Execute()
            {
                using var repo = new Repository(RepositoryPath);
                Versions versions = GetVersions(repo);

                if (Verbose)
                {
                    Console.WriteLine($"NextMajor: {versions.getNextMajor()}");
                    Console.WriteLine($"NextMinor: {versions.getNextMinor()}");
                    Console.WriteLine($"NextPatch: {versions.getNextPatch()}");
                }

                Commit c0 = repo.Commits.ElementAt(0);

                if (IsMajor(c0.MessageShort))
                {
                    Console.WriteLine(versions.getNextMajor());
                }
                else if (IsMinor(c0.MessageShort))
                {
                    Console.WriteLine(versions.getNextMinor());
                }
                else if (IsPatch(c0.MessageShort) || !Explicit)
                {
                    Console.WriteLine(versions.getNextPatch());
                }
                else
                {
                    Environment.Exit(-1);
                }
            }
        }

        [Verb("generate", HelpText = "Generates a new tag and version branch if necessary.")]
        public class Generate : VersionCommands
        {
            public override void Execute()
            {
                using var repo = new Repository(RepositoryPath);
                var versions = GetVersions(repo);

                if (Verbose)
                {
                    Console.WriteLine($"NextMajor: {versions.getNextMajor()}");
                    Console.WriteLine($"NextMinor: {versions.getNextMinor()}");
                    Console.WriteLine($"NextPatch: {versions.getNextPatch()}");
                }



                var current = repo.Commits.ElementAt(0);
                var previous = repo.Commits.ElementAt(1);

                if (IsMajor(current.MessageShort))
                {
                    repo.Branches.Add($"v{--versions.getNextMajor().Major}", previous.Sha);
                    repo.ApplyTag(versions.getNextMajor().ToString(), current.Sha);
                }
                else if (IsMinor(current.MessageShort))
                {
                    repo.ApplyTag(versions.getNextMinor().ToString(), current.Sha);
                }
                else if (IsPatch(current.MessageShort) || !Explicit)
                {
                    repo.ApplyTag(versions.getNextPatch().ToString(), current.Sha);
                }
                else
                {
                    Environment.Exit(-1);
                }
            }
        }

        private static bool IsMajor(string input)
        {
            const string pattern = @"^(\[MAJOR\])|(\[BREAKING\])";

            return Regex.IsMatch(input, pattern);
        }

        private static bool IsMinor(string input)
        {
            const string pattern = @"^(\[MINOR\])|(\[FEATURE\])";

            return Regex.IsMatch(input, pattern);
        }

        private static bool IsPatch(string input)
        {
            const string pattern = @"^(\[PATCH\])|(\[FIX\])";

            return Regex.IsMatch(input, pattern);
        }
    }
}