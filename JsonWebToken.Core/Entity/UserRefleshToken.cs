using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Core.Entity
{
    public class UserRefleshToken:BaseEntity
    {
        public string? UserID { get; set; }
        public string? Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
