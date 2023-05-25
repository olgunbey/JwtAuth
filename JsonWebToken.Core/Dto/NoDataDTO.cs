using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Core.Dto
{
    public class NoDataDTO
    {
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
    }
}
