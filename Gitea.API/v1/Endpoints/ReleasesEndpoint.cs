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
                var resp = await rest.GetAsync("repos/" + HttpUtility.UrlEncode(owner) + "/" + HttpUtility.UrlEncode(name) + "/releases");
                await CheckResponse(resp);

                var json = await resp.Content.ReadAsStringAsync();

                var repo = JsonConvert.DeserializeObject<List<Release>>
                    (
                       json
                    );

                return repo;

            }
        }

    }
}
