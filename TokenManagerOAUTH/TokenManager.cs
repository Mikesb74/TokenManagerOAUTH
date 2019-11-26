using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace TokenManagerOAUTH
{
    using TokenManagerOAUTH.JSONModels;
    public static class TokenManager
    {
        //private readonly static NLog.Logger logger;
        public static TokenModel GetCurrentOAuthToken(TokenRequestModel tokenRequest)
        {
            return GetOAuthToken(tokenRequest);
        }
        private static List<TokenRecord> _ActiveTokenList;
        static TokenManager()
        {
            //logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            _ActiveTokenList = new List<TokenRecord>();
        }
        private static TokenModel GetOAuthToken(TokenRequestModel tokenRequest)
        {
            //Look Up Token Record from Locally by Client ID
            var item = _ActiveTokenList.FirstOrDefault(t => t.TokenRequest.OAuthCreds.ClientId == tokenRequest.OAuthCreds.ClientId);

            if (item == null)
            {
                LogInfo($"Didn't find a token record for ClientId {tokenRequest.OAuthCreds.ClientId}", tokenRequest);
                //If Record Not Create New Token Record 
                return GetNewOAuthToken(tokenRequest);
            }
            else
            {
                if (item.Expires > DateTime.Now)
                {
                    LogInfo($"Active token record for ClientId {tokenRequest.OAuthCreds.ClientId} found", tokenRequest);
                    //Use Stored Token
                    return item.Token;
                }
                else
                {
                    LogInfo($"Expired token record for ClientId {tokenRequest.OAuthCreds.ClientId} found", tokenRequest);
                    //Renew Token Record
                    return ReNewToken(item);
                }
            }
        }

        private static void LogInfo(string message, TokenRequestModel tokenRequest)
        {
            //Infrastructure.LoggingService.Information(message, logger, tokenRequest.RequestId);
        }

        private static TokenModel ReNewToken(TokenRecord record)
        {
            _ActiveTokenList.Remove(record);
            record.Token = GetNewOAuthToken(record.TokenRequest);

            return record.Token;
        }

        private static TokenModel GetNewOAuthToken(TokenRequestModel tokenRequest)
        {
            TokenRecord record = TokenGateway.GetNewOAuthToken(tokenRequest);
            _ActiveTokenList.Add(record);
            return record.Token;
        }
    }

    public class SixcelOAuthTokenCreds 
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
   
}
namespace TokenManagerOAUTH.JSONModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class TokenModel
    {
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken { get; set; }

        [JsonProperty("token_type", NullValueHandling = NullValueHandling.Ignore)]
        public string TokenType { get; set; }

        [JsonProperty("expires_in", NullValueHandling = NullValueHandling.Ignore)]
        public double ExpiresIn { get; set; }
    }
    public partial class TokenModel
    {
        public static TokenModel TokenFromJson(string json) => JsonConvert.DeserializeObject<TokenModel>(json, Converter.Settings);
    }
    public class SixcelJSONModels
    {
    }
    public static partial class Serialize
    {
        public static string ToJson(this TokenModel self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = System.Globalization.DateTimeStyles.AssumeUniversal }
            },
        };
    }

}