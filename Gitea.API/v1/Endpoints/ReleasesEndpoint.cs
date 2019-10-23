using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Gitea.API.v1.Types;
using System.Globalization;
using System.IO; 
using System.Net.Http.Headers;

namespace Gitea.API.v1.Endpoints
{
    public class ReleasesEndpoint : EndpointBase
    {

        public ReleasesEndpoint(Client client) : base(client)
        { }

        public void addAttatchment(string owner, string name, string tag, string filename)
        {
            var job = addAttatchmentAsync(owner, name, tag, filename);
            job.Wait();
        }



        public async Task addAttatchmentAsync(string owner, string name, string tag, string filename)
        {
            var release = this.Get(owner, name).Find(r => r.Tag_name == tag);

            using (var rest = Client.CreateBaseClient())
            {

                var requestContent = new MultipartFormDataContent();
                var archiveContents = File.ReadAllBytes(filename);
                var imageContent = new ByteArrayContent(archiveContents);


                requestContent.Add( imageContent, "attachment", filename);

                var resp = await rest.PostAsync("repos/" + HttpUtility.UrlEncode(owner) + "/" + HttpUtility.UrlEncode(name) + "/releases/" + HttpUtility.UrlEncode(release.Id.ToString()) + "/assets", requestContent);

                await CheckResponse(resp);
            }
        }

        public List<Release> Get(string owner, string name)
        {
            var repo = this.GetAsync(owner, name);
            repo.Wait();

            return repo.Result;
        }


        public async Task<List<Release>> GetAsync(string owner, string name)
        {

            using (var rest = Client.CreateBaseClient())
            {
                Console.WriteLine("GetAsync base Adress : "+  rest.BaseAddress);
                Console.WriteLine("GetAsync Name : " + name);                  
                Console.WriteLine("GetAsync owner : " + owner);                  


                string request = "repos/" + HttpUtility.UrlEncode(owner) + "/" + HttpUtility.UrlEncode(name) + "/releases";
                 
                var resp = await rest.GetAsync(request);
                await CheckResponse(resp);

                var json = await resp.Content.ReadAsStringAsync();                 
                var repo = JsonConvert.DeserializeObject<List<Release>>
                    (
                       json
                    );


                return repo;

            }
        }

        public void updateRelease(string owner, string repo, string TagName, string Body, string NewName = null, bool Prerelease = false, bool Draftrelease = false)
        {

            Console.WriteLine("IchwarHier"); 
            var job = updateReleaseAsync(owner, repo, TagName, Body, NewName, Prerelease, Draftrelease );
            job.Wait();

        }

        public async Task updateReleaseAsync (string Owner, string Name,  string TagName, string Body, string NewName = null, bool Prerelease = false, bool Draftrelease = false)
        {
            var release = this.Get(Owner, Name).Find(r => r.Tag_name == TagName);             
            Console.WriteLine("updateReleaseAsync release ID: " + release.Id.ToString());
            using (var rest = Client.CreateBaseClient())
            {
                Release release1 = new Release();
                release1.Draft = Draftrelease;
                release1.Body = Body;
                release1.Name = NewName;
                release1.Prerelease = Prerelease;
                release1.Tag_name = TagName;

                var json = JsonConvert.SerializeObject(release1);
                Console.WriteLine("updatereleasesAsync json: " + json);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                string request = "repos/" + HttpUtility.UrlEncode(Owner) + "/" + HttpUtility.UrlEncode(Name) + "/releases/" + HttpUtility.UrlEncode(release.Id.ToString());

                Console.WriteLine("updateReleaseAsync Request: " + request);                
                var resp = await rest.PostAsync(request, httpContent);            
                 
                await CheckResponse(resp);
                Console.WriteLine("updateReleaseAsync after Post: it's done !!!");
            }
        }

            

                

    }
}
