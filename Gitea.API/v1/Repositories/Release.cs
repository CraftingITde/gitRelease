using System;
using System.Collections.Generic;
using System.Text;

namespace Gitea.API.v1.Repositories
{
    public class Release
    {

        public int id { get; set; }
        public string body { get; set; }
        public bool draft { get; set; }
        public string name { get; set; }
        public bool prerelease { get; set; }
        public string tag_name { get; set; }

    }
}
