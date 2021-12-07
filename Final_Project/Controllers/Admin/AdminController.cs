using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace Final_Project.Controllers.Admin
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IUserRepository UserRepository;
        public AdminController(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            UserRepository = userRepository;

        }


        [HttpPost("addadmin")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signupModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.SignUp(signupModel);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            var adminRole = new AddRoleModel
            {
                UserID = result.User_ID,
                Role = "Admin"
            };

            await AddRole(adminRole);
            return Ok(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.Login(loginModel);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("addrole")]
        private async Task<IActionResult> AddRole(AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.AddRole(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
    }

}
