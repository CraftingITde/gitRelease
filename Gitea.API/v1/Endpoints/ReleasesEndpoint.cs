using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Gitea.API.v1.Endpoints
{
    public class ReleasesEndpoint : EndpointBase
    {

        public ReleasesEndpoint(Client client) : base(client)
        { }

       /* public async Task<User> GetCurrent()
        {
            using (var rest = Client.CreateBaseClient())
            {
                var resp = await rest.GetAsync("user");

                return await CreateUserObject(resp);
            }
        } */
    }
}
