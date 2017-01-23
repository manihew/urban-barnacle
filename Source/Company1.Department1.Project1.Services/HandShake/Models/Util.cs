using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;

namespace Company1.Department1.Project1.Services.HandShake.Models
{
    public class Util
    {
        public static String TimeStamp
        {
            get
            {
                return Convert.ToUInt64(DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString();
            }

        }

        public static String NewAppKey
        {
            get
            {
                using (var rng = new RNGCryptoServiceProvider())
                {
                    byte[] randomKey = new byte[32];
                    rng.GetBytes(randomKey);
                    return Convert.ToBase64String(randomKey);
                }
            }
        }

        public static String NewAppId
        {
            get { return Guid.NewGuid().ToString("N"); }
        }

        public static String AppendQuery(String baseUrl, String key, String value)
        {
            var builder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query[key] = HttpUtility.UrlEncode(value);
            builder.Query = query.ToString();
            return builder.ToString();
        }

        public static String AppendQuery(String baseUrl, IDictionary<String, String> dict)
        {
            var builder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var item in dict)
            {
                query[item.Key] = HttpUtility.UrlEncode(item.Value);
            }
            builder.Query = query.ToString();
            return builder.ToString();
        }

        public static String AppendUrl(String baseUrl, String appendage)
        {
            return "";
        }
    }
}
