using JsonWebToken.Core.Dto;
using JsonWebToken.Core.Entity;
using JsonWebToken.Core.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> userManager;
        public UserService(UserManager<UserApp> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<ResponseDTO<NoDataDTO>> CreateUserAsync(CreateUserDTO userAppDTO)
        {
            UserApp userApp = new();
            userApp.UserName = userAppDTO.UserName;
            userApp.Email= userAppDTO.Email;
            var IdenityResult = await  userManager.CreateAsync(userApp,userAppDTO.Password);
            if(IdenityResult.Succeeded)
            {
                return ResponseDTO<NoDataDTO>.Succcess(200);
            }
            return ResponseDTO<NoDataDTO>.Fail("oluşturulamadı", 200);
        }

        public async Task<ResponseDTO<UserAppDTO>> GetUserByName(string username)
        {
          UserApp? userApp=  await userManager.FindByNameAsync(username);
            if(userApp==null)
            {
                return ResponseDTO<UserAppDTO>.Fail("bu kullanıcı yok", 200);
            }
            UserAppDTO userAppDTO = new UserAppDTO();
            userAppDTO.Email = userApp.Email!;
            userAppDTO.Name = userApp.UserName!;
            userAppDTO.Id = userApp.Id;
            return ResponseDTO<UserAppDTO>.Succcess(userAppDTO, 200);
        }
    }
}
