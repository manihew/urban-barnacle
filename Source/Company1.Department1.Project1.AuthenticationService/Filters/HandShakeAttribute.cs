using Company1.Department1.Project1.AuthenticationService.Context;
using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;


namespace Company1.Department1.Project1.AuthenticationService.Filters
{
    /// <summary>
    /// Hand Shake Attribute
    /// OAUTH implementation of authentication
    /// </summary>
    public class HandShakeAttribute : Attribute, IAuthenticationFilter
    {
        private readonly UInt64 _MaxAgeInSeconds = 300;

        /// <summary>
        /// Authenticate Request
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var req = context.Request;

            var uri = System.Web.HttpUtility.UrlEncode(req.RequestUri.AbsoluteUri.ToLower());
            var method = req.Method.Method;

            if (req.Headers.Authorization == null || String.Compare("cus", req.Headers.Authorization.Scheme, StringComparison.OrdinalIgnoreCase) != 0)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            var hdr = req.Headers.Authorization.Parameter.Split(':');

            if (hdr.Length != 4)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            var appId = hdr[0];
            var bodyContent = hdr[1];
            var random = hdr[2];
            var timestamp = hdr[3];

            context.Request.Properties.Add("AppId", appId);

            var allowedApps = SingletonContext.Instance.Applications;

            if (!allowedApps.ContainsKey(appId))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            if (SingletonContext.Instance.RequestLogs.ContainsKey(random))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            var current = Convert.ToUInt64(DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
            var requested = Convert.ToUInt64(timestamp);

            if ((current - requested) > _MaxAgeInSeconds)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.FromResult(0);
            }

            SingletonContext.Instance.RequestLogs[random] = timestamp;

            var sharedKey = allowedApps[appId];

            var body = String.Empty;
            if (req.Content != null)
            {
                var content = req.Content.ReadAsByteArrayAsync().Result;
                if (content.Length != 0)
                {
                    body = Convert.ToBase64String(MD5.Create().ComputeHash(content));
                }
            }

            var formattedBodyContent = String.Format("{0}{1}{2}{3}{4}{5}", appId, method, uri, timestamp, random, body);

            var hashedAppKey = Convert.FromBase64String(sharedKey);

            var bodyContentBytes = Encoding.UTF8.GetBytes(formattedBodyContent);

            using (HMACSHA256 hmac = new HMACSHA256(hashedAppKey))
            {
                var requestContent = Convert.ToBase64String(hmac.ComputeHash(bodyContentBytes));
                if (String.Compare(requestContent, bodyContent, StringComparison.Ordinal) != 0)
                {
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                    return Task.FromResult(0);
                }
            }

            context.Principal = new GenericPrincipal(new GenericIdentity(appId), null);
            req.Headers.Add("Token", appId);

            return Task.FromResult(0);
        }

        /// <summary>
        /// Challenge
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            // context.Result = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            return Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}