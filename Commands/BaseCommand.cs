using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace gitRelease.Commands
{
    public abstract class BaseCommand
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        public abstract void Execute();
    }
}
