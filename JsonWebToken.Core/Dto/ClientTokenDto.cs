using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Core.Dto
{
    public class ClientTokenDto
    {
        public string? AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
    }
}
