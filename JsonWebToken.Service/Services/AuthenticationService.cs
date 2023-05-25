using JsonWebToken.Core.Dto;
using JsonWebToken.Core.Entity;
using JsonWebToken.Core.IRepository;
using JsonWebToken.Core.IService;
using JsonWebToken.Core.IUnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _client;
        private ITokenService _tokenService;
        private UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefleshToken> _genericRepository;
        public AuthenticationService(IOptions<List<Client>> client, ITokenService tokenService , UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefleshToken> genericRepository)
        {
            _client = client.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public Task<ResponseDTO<ClientTokenDto>> ClientCreateTokenAsync(ClientLoginDTO loginDTO)
        {
           var client= _client.SingleOrDefault(x => x.Id == loginDTO.CLientId && x.Secret == loginDTO.ClientSecret);
            if(client==null)
            {
                return Task.FromResult(ResponseDTO<ClientTokenDto>.Fail("Hata!", 200));
            }
            var token = _tokenService.CreateTokenByClient(client);
            return Task.FromResult(ResponseDTO<ClientTokenDto>.Succcess(token, 200));



        }

        public async Task<ResponseDTO<TokenDto>> CreateTokenAsync(LoginDTO loginDTO)
        {
            UserApp? user =await _userManager.FindByEmailAsync(loginDTO.Email!);
            if (user == null)
            {
                return ResponseDTO<TokenDto>.Fail("Bu kullanıcı bulunamadı", 200);
            }
            if(!(await _userManager.CheckPasswordAsync(user,loginDTO.Password)))
            {
                return ResponseDTO<TokenDto>.Fail("Bu kullanıcı bulunamadı", 200);
            }
            var token = _tokenService.CreateToken(user);

            var userRefleshToken = (await _genericRepository.GetAll(x => x.UserID == user.Id)).SingleOrDefault();
            if(userRefleshToken == null)
            {
                await _genericRepository.AddAsync(new() { UserID = user.Id, Code = token.RefleshToken, Expiration = token.RefleshTokenExpiration });
            }
            else
            {
                userRefleshToken.Code = token.RefleshToken;
                userRefleshToken.Expiration = token.RefleshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();
            return ResponseDTO<TokenDto>.Succcess(token, 200);
        }

        public async Task<ResponseDTO<TokenDto>> CreateTokenByRefleshToken(string refleshtoken)
        {
            var refleshTokens = (await _genericRepository.GetAll(x => x.Code == refleshtoken)).SingleOrDefault();
            if(refleshTokens == null)
            {
                return ResponseDTO<TokenDto>.Fail("Reflesh token yok", 200);
            }
          UserApp? userApp= await _userManager.FindByIdAsync(refleshTokens.UserID!);
            if(userApp == null)
            {
                return ResponseDTO<TokenDto>.Fail("KUllanici yok", 200);
            }
            var token = _tokenService.CreateToken(userApp);
            refleshTokens.Code = token.RefleshToken;
            refleshTokens.Expiration = token.RefleshTokenExpiration;

            await _unitOfWork.CommitAsync();
            return ResponseDTO<TokenDto>.Succcess(token, 200);


        }

        public async Task<ResponseDTO<NoDataDTO>> RevokeRefleshToken(string reflestoken)
        {
       UserRefleshToken? refleshTokens=  (await _genericRepository.GetAll(x=>x.Code==reflestoken)).SingleOrDefault();

            if(refleshTokens==null)
            {
                return ResponseDTO<NoDataDTO>.Fail("token yok", 200);
            }
          await  _genericRepository.DeleteAsync(refleshTokens);
            await _unitOfWork.CommitAsync();
            return ResponseDTO<NoDataDTO>.Succcess(200);
        }
    }
}
