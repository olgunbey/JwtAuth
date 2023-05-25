using JsonWebToken.Core.Dto;
using JsonWebToken.Core.IService;
using Microsoft.AspNetCore.Mvc;

namespace JsonWebToken.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            var response=  await  userService.CreateUserAsync(createUserDTO);
            return new ResponseDTO<NoDataDTO>.ResponseStruct<NoDataDTO>().ResponsesDTO(response);
        }
        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser(string name)
        {
            var response=  await userService.GetUserByName(name);
            return new ResponseDTO<UserAppDTO>.ResponseStruct<UserAppDTO>().ResponsesDTO(response);
        }
    }
}
