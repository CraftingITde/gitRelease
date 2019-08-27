using Gitea.API.v1.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Gitea.API.v1.Endpoints
{
    /// <summary>
    /// An endpoint for an user repository.
    /// </summary>
    public class RepositoryEndpoint : EndpointBase
    {
        internal RepositoryEndpoint(Client client) : base (client)
        {
            Release = new ReleasesEndpoint(this.Client);
        }

        public ReleasesEndpoint Release { get; private set; }



        public Repository Get(string owner, string name)
        {
            var repo = this.GetAsync(owner, name);
            repo.Wait();

            return repo.Result;
        }


        public async Task<Repository> GetAsync(string owner, string name)
        {

            using (var rest = Client.CreateBaseClient())
            {
                var resp = await rest.GetAsync("repos/" + HttpUtility.UrlEncode(owner) + "/" + HttpUtility.UrlEncode(name));
                await CheckResponse(resp);

                var json = await resp.Content.ReadAsStringAsync();

                var repo = JsonConvert.DeserializeObject<Repository>
                    (
                       json
                    );

                return repo;

            }
        }

      
    }
}