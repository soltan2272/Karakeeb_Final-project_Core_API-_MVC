using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace Final_Project.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
     
        IUserRepository UserRepository;
        public UserController(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            UserRepository = userRepository;
           
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel signupModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.SignUp(signupModel);
            if(!result.IsAuthenticated)
                return BadRequest(result.Message);
          


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

    
    }
}
