using JsonWebToken.Core.Dto;
using JsonWebToken.Core.IService;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace JsonWebToken.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken(LoginDTO loginDTO)
        {
            var response = await authenticationService.CreateTokenAsync(loginDTO);
            return new ResponseDTO<TokenDto>.ResponseStruct<TokenDto>().ResponsesDTO(response);
        }
        [HttpPost("CreateTokenByClient")]
        public async Task<IActionResult> CreateTokenByClient(ClientLoginDTO clientLoginDTO)
        {
          var response= await authenticationService.ClientCreateTokenAsync(clientLoginDTO);
            return new ResponseDTO<ClientTokenDto>.ResponseStruct<ClientTokenDto>().ResponsesDTO(response);
        }
        [HttpPost("RevokeRefleshToken")]
        public async Task<IActionResult> RevokeRefleshToken(RefleshTokenDto refleshToken)
        {
         var response= await authenticationService.RevokeRefleshToken(refleshToken.Token);

            return new ResponseDTO<NoDataDTO>.ResponseStruct<NoDataDTO>().ResponsesDTO(response);
        }
        [HttpPost("CreateTokenByRefleshToken")]
        public async Task<IActionResult> CreateTokenByRefleshToken(RefleshTokenDto refleshToken)
        {
            var response = await authenticationService.CreateTokenByRefleshToken(refleshToken.Token);

            return new ResponseDTO<TokenDto>.ResponseStruct<TokenDto>().ResponsesDTO(response);
        }
    }
}
