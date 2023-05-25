using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Core.Dto
{
    public class ClientLoginDTO
    {
        public string? CLientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
