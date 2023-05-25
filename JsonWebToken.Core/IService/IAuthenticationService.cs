using JsonWebToken.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Core.IService
{
    public interface IAuthenticationService
    {
        Task<ResponseDTO<TokenDto>> CreateTokenAsync(LoginDTO loginDTO);
        Task<ResponseDTO<TokenDto>> CreateTokenByRefleshToken(string refleshtoken); //refleshtoken alıp yeniden access token uretır
        Task<ResponseDTO<NoDataDTO>> RevokeRefleshToken(string reflestoken);
        Task<ResponseDTO<ClientTokenDto>> ClientCreateTokenAsync(ClientLoginDTO loginDTO);
    }
}
