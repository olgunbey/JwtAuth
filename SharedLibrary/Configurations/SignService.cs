using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configurations
{
    public static class SignService
    {
        public static SecurityKey GetSimetrikKey(string keys)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keys));
        }
    }
}
