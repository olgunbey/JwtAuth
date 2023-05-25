using JsonWebToken.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Core.IService
{
    public interface IUserService
    {
        Task<ResponseDTO<NoDataDTO>> CreateUserAsync(CreateUserDTO userAppDTO);
        Task<ResponseDTO<UserAppDTO>> GetUserByName(string username);
    }
}
