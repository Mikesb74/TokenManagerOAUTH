using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenManagerOAUTH
{
    public partial class TokenRecord
    {
        public TokenRequestModel TokenRequest { get; set; }
        public TokenModel Token { get; set; }
        public DateTime Expires { get; set; }

        public TokenRecord(TokenModel token, TokenRequestModel request)
        {
            Token = token;
            Expires = DateTime.Now.AddSeconds(token.ExpiresIn);
            TokenRequest = request;
        }
    }

}
