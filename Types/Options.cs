using System;
using CommandLine;

namespace gitRelease.Types
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }



    public class gitBaseSettings : Options
    {
        [Option('t', "tag", Required = true, HelpText = "The tag vor the Release")]
        public string Tag { get; set; }
    }


    [Verb("github", HelpText = "Add file contents to the index.")]
    public class githubOptions : gitBaseSettings
    { //normal options here

    }
    [Verb("gittea", HelpText = "Record changes to the repository.")]
    public class giteaOptions : gitBaseSettings
    { //normal options here

    }
}

