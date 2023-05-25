using JsonWebToken.Core.Dto;
using JsonWebToken.Core.Entity;
using JsonWebToken.Core.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;

namespace JsonWebToken.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption options;
        public TokenService(UserManager<UserApp> userManager,IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            this.options = options.Value;
        }
        private string CreateRefleshToken()
        {
            var numberByte = new Byte[32];
            var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);

           return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(UserApp userApp,List<string> audience)
        {
            var userList = new List<Claim>()
           {

               new Claim(ClaimTypes.NameIdentifier,userApp.Id),
               new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
               new Claim(ClaimTypes.Name,userApp.UserName),
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
           };

            userList.AddRange(audience.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claim=new List<Claim>()
            {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //token'ın id'si
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString())
            };
            claim.AddRange(client.Audience!.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return claim;
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(15);
            var securityKey = SignService.GetSimetrikKey("mysecretkeymysecretkeymysecretkeymysecretkeymysecretkey");

            SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new(
                issuer:options.Issuer,
                expires:accessTokenExpiration,
                notBefore:DateTime.Now,
                claims: GetClaims(userApp,options.Audience),
                signingCredentials:signingCredentials
                );
            var handler =new JwtSecurityTokenHandler();
            var token= handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto()
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
                RefleshToken = CreateRefleshToken(),
                RefleshTokenExpiration = DateTime.Now.AddMinutes(15)
            };
            return tokenDto;

        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(15);
            var securityKey = SignService.GetSimetrikKey("mysecretkeymysecretkeymysecretkeymysecretkeymysecretkey");

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new(
                issuer: "www.authserver.com",
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            var ClienttokenDto = new ClientTokenDto()
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
            };
            return ClienttokenDto;
        }

        
    }
}
