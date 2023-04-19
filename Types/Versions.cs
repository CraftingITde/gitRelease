using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace gitRelease.Types
{
    public class Versions
    {
        private string _currentBranch;

        public string MasterBranchName { get; set; } = "master";
        private List<Version> _versions = new List<Version>() {
            new Version(0, 0, 0)
        };

        public Versions(string currentBrach)
        {
            _currentBranch = currentBrach;
        }

        public bool addTag(string tagName)
        {

            const string _exp = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)";
            if (Regex.IsMatch(tagName, _exp))
            {
                var version = tagName.Split(".");
                addVersion(new Version(tagName));
                return true;
            }
            return false;
        }

        public bool addBranch(string branchName)
        {
            var branchVersion = getBrachVersion(branchName);

            if (branchVersion >= 0)
            {
                addVersion(new Version(branchVersion));
                return true;
            }
            return false;
        }

        public Version getNextMajor()
        {
            int max = _versions.Max(v => v.Major);
            return new Version(++max);
        }

        public Version getNextMinor()
        {
            if (_currentBranch == MasterBranchName)
            {
                int maxMajor = this._versions.Max(v => v.Major);
                int maxMinor = this._versions.Where(v => v.Major == maxMajor).Max(v => v.Minor);

                return new Version(maxMajor, ++maxMinor);
            }
            else if (getBrachVersion(_currentBranch) >= 0)
            {
                int branchVersion = getBrachVersion(_currentBranch);
                int maxMinor = this._versions.Where(v => v.Major == branchVersion).Max(v => v.Minor);

                return new Version(branchVersion, ++maxMinor);
            }
            else
            {
                return null;
            }
        }

        public Version getNextPatch()
        {
            if (_currentBranch == MasterBranchName)
            {
                int maxMajor = this._versions.Max(v => v.Major);
                int maxMinor = this._versions.Where(v => v.Major == maxMajor).Max(v => v.Minor);
                int maxPatch = this._versions.Where(v => v.Major == maxMajor && v.Minor == maxMinor).Max(v => v.Patch);

                return new Version(maxMajor, maxMinor, ++maxPatch);
            }
            else if (getBrachVersion(_currentBranch) >= 0)
            {
                int branchVersion = getBrachVersion(_currentBranch);
                int maxMinor = this._versions.Where(v => v.Major == branchVersion).Max(v => v.Minor);
                int maxPatch = this._versions.Where(v => v.Major == branchVersion && v.Minor == maxMinor).Max(v => v.Patch);

                return new Version(branchVersion, maxMinor, ++maxPatch);
            }
            else
            {
                return null;
            }
        }

        private void addVersion(Version version)
        {
            if (!_versions.Any(v => v.ToString() == version.ToString()))
            {
                _versions.Add(version);
            }
        }

        private int getBrachVersion(string branchName)
        {
            const string _exp = @"^v(0|[1-9]\d*)";

            if (Regex.IsMatch(branchName, _exp))
            {
                return int.Parse(branchName.Replace("v", ""));
            }
            return -1;
        }
    }

    public class Version
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }

        public Version(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public Version(int major, int minor) : this(major, minor, 0) { }

        public Version(int major) : this(major, 0) { }


        public Version(string versionStr)
        {
            var strParts = versionStr.Split(".");

            Major = int.Parse(strParts[0]);
            Minor = int.Parse(strParts[1]);
            Patch = int.Parse(strParts[2]);
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }
    }
}
