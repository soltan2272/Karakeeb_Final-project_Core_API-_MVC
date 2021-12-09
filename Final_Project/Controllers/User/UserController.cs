using Microsoft.AspNetCore.Cors;

﻿using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.User;
using ViewModels.Userr;


namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
     
        IUserRepository UserRepository;
        IGenericRepostory<Order> OrderRepo;
        IGenericRepostory<Feedback> FeedbackRepo;

        Project_Context Context;

        IUnitOfWork UnitOfWork;
        ResultViewModel result = new ResultViewModel();
        
        public UserController(IUserRepository userRepository,
            IUnitOfWork unitOfWork, Project_Context context)
        {
            UnitOfWork = unitOfWork;
            UserRepository = userRepository;
            OrderRepo = UnitOfWork.GetOrderRepo();
            FeedbackRepo = UnitOfWork.GetFeedbackRepo();
            Context = context;
            
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

        [HttpGet("")]
        public ResultViewModel Get()
        {

            result.Message = "All Orders";
            result.Data = OrderRepo.Get().Select(p => p.OrderViewModel());
            return result;
        }

        [HttpGet("getusers")]
        public async Task<dynamic> GetAllUsers()
        {

            var res = await UserRepository.GetUsersAsync();
            result.Data = res;
            result.Message = "Succedd";
            result.ISuccessed = true;
            return result;
        }

        [HttpGet("getuser/{id}")]
        public async Task<ResultViewModel> GetUserByID(int id)
        {
            ViewUser res = await UserRepository.GetUserBYIDAsync(id);
           // User view = usr.ToModel();
           result.Data = res;
            result.Message = "Succedd";
            result.ISuccessed = true;
            return result;
        }

        [HttpDelete("deleteuser/{id}")]
        public async Task<dynamic> DeleteUser(int id)
        {
            var res = await UserRepository.DeleteUser(id);
            result.ISuccessed = true;
            result.Data = res;
            result.Message = "Deleted Successfully";
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
