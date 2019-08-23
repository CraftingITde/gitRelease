using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Gitea.API.v1
{
    /// <summary>
    /// An API authorizer for Token authentification.
    /// </summary>
    public class TokenAuth : IAuthorizer
    {
        /// <inheritdoc />
        public void PrepareClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                (
                    "token",
                    Token
                );
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Token
        {
            get;
            set;
        }
    }
}