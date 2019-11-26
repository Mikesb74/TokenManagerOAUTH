using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenManagerOAUTH
{
    public partial class TokenRequestModel
    {

        public SixcelOAuthTokenCreds OAuthCreds { get; set; }
        public string GrantType { get; set; }
        public string Scope { get; set; }
        public string TokenURL { get; set; }
        public int RequestId { get; set; }
    }


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
