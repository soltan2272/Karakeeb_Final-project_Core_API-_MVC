using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
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
        IGenericRepostory<Order> OrderRepo;
        IGenericRepostory<Feedback> FeedbackRepo;

        IUnitOfWork UnitOfWork;
        ResultViewModel result = new ResultViewModel();

        public UserController(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            UserRepository = userRepository;
            OrderRepo = UnitOfWork.GetOrderRepo();
            FeedbackRepo = UnitOfWork.GetFeedbackRepo();
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


        [HttpPost("addorder")]
        public ResultViewModel AddOrder(Order order)
        {
            result.Message = "Add User Order";

            OrderRepo.Add(order);
            UnitOfWork.Save();
            result.Data = order;

            return result;

        }

        [HttpDelete("deleteorder")]
        public ResultViewModel DeleteOrder(int id)
        {
            result.Message = " Order Deleted";
            result.Data = OrderRepo.GetByID(id);
            OrderRepo.Remove(new Order { ID = id});
            UnitOfWork.Save();

            return result;

        }

        [HttpPut("editorder")]
        public ResultViewModel EditOrder(Order order)
        {

            result.Message = "edit order";
            result.Data = order;
            OrderRepo.Update(order);
            UnitOfWork.Save();
            return result;
        }

        [HttpPost("addfeedback")]
        public ResultViewModel AddFeedback(Feedback feedback)
        {
            result.Message = "Add User Feedback";

            FeedbackRepo.Add(feedback);
            UnitOfWork.Save();
            result.Data = feedback;

            return result;

        }   






    }
}
