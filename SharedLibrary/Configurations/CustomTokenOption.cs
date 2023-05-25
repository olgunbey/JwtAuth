using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configurations
{
    public class CustomTokenOption
    {
        public string Issuer { get; set; }
        public List<string> Audience { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefleshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
