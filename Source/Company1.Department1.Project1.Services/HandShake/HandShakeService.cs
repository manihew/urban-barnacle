using Company1.Department1.Project1.Services.HandShake.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Company1.Department1.Project1.Services.HandShake
{
    /// <summary>
    /// HandShakeService for OAUTH implementation
    /// </summary>
    public class HandShakeService : DelegatingHandler
    {
        private String _AppId, _AppKey;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        public HandShakeService(String appId, String appKey)
        {
            _AppId = appId;
            _AppKey = appKey;
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = HttpUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToLower());
            var time = Util.TimeStamp;
            var random = Util.NewAppId;
            var method = request.Method.Method;

            var body = String.Empty;
            if (request.Content != null)
            {
                var content = await request.Content.ReadAsByteArrayAsync();
                body = Convert.ToBase64String(MD5.Create().ComputeHash(content));
            }

            var formattedBodyContent = String.Format("{0}{1}{2}{3}{4}{5}", _AppId, method, uri, time, random, body);

            var hashedAppKey = Convert.FromBase64String(_AppKey);

            var bodyContent = Encoding.UTF8.GetBytes(formattedBodyContent);

            using (HMACSHA256 hmac = new HMACSHA256(hashedAppKey))
            {
                var requestContent = Convert.ToBase64String(hmac.ComputeHash(bodyContent));
                request.Headers.Authorization = new AuthenticationHeaderValue("cus", string.Format("{0}:{1}:{2}:{3}", _AppId, requestContent, random, time));
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
