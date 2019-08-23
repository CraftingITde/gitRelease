using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace gitRelease.Types
{
    public class GithubApi
    {

        private GitHubClient client = new GitHubClient(new ProductHeaderValue("gitrelease"));

        private string owner;
        private string repo;

        public GithubApi(string Token, string owner, string repo)
        {
            var tokenAuth = new Credentials(Token);
            client.Credentials = tokenAuth;
            this.owner = owner;
            this.repo = repo;
        }


        public void getReleases()
        {
            var task = client.Repository.Release.GetAll(owner, repo);
            task.Wait();
            var releases = task.Result;
            var latest = releases[0];
            Console.WriteLine(
                "The latest release is tagged at {0} and is named {1}",
                latest.TagName,
                latest.Name);
        }

        public void createRelease(string TagName, string Body, string Name = null, bool Prerelease = false, bool Draftrelease = false)
        {
            try
            {
                var newRelease = new NewRelease(TagName);
                newRelease.Name = Name == null ? TagName : Name;
                newRelease.Body = Body.Replace("\\n", Environment.NewLine); ;
                newRelease.Prerelease = Prerelease;
                newRelease.Draft = Draftrelease;

                var result = client.Repository.Release.Create(owner, repo, newRelease);
                result.Wait();
            }
            catch
            {
                Console.WriteLine("Error creating Release");
                Environment.Exit(-1);
            }

        }

        public void updateRelease(string TagName, string Body, string Name = null, bool Prerelease = false, bool Draftrelease = false)
        {
            try
            {
                var release = client.Repository.Release.Get(owner, repo, TagName);
                release.Wait();

                var updateRelease = release.Result.ToUpdate();
                updateRelease.Draft = Draftrelease;
                updateRelease.Prerelease = Prerelease;
                updateRelease.Name = Name;
                updateRelease.Body = Body.Replace("\\n", Environment.NewLine);

                var result = client.Repository.Release.Edit(owner, repo, release.Result.Id, updateRelease);
                result.Wait();
            }
            catch
            {
                Console.WriteLine("Error updating Release");
                Environment.Exit(-1);
            }

        }


        public void updateReleaseBody(string TagName, string lines, string Body = null)
        {
            try
            {
                var release = client.Repository.Release.Get(owner, repo, TagName);
                release.Wait();


                if (Body == null)
                    Body = release.Result.Body;


                Body += lines == null ? "" : lines;

                var updateRelease = release.Result.ToUpdate();
                updateRelease.Body = Body.Replace("\\n", Environment.NewLine); ;

                var result = client.Repository.Release.Edit(owner, repo, release.Result.Id, updateRelease);
                result.Wait();
            }
            catch
            {
                Console.WriteLine("Error Updating Body");
                Environment.Exit(-1);
            }
        }




        public void uploadAsset(string TagName, string FileName)
        {
            try
            {
                using (var archiveContents = File.OpenRead(FileName))
                {
                    var assetUpload = new ReleaseAssetUpload()
                    {
                        FileName = FileName,
                        RawData = archiveContents,
                        ContentType = "application/octet-stream"
                    };
                    var release = client.Repository.Release.Get(owner, repo, TagName);
                    release.Wait();
                    var asset = client.Repository.Release.UploadAsset(release.Result, assetUpload);
                    asset.Wait();
                }
            }
            catch
            {
                Console.WriteLine("Error uploading Asset");
                Environment.Exit(-1);
            }
        }

    }
}
