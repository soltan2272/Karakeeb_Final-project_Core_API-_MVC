using Microsoft.AspNetCore.Cors;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ViewModels;

namespace Final_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        IGenericRepostory<Product> ProductRepo;

        IGenericRepostory<Store> StoreRepo;
        IUnitOfWork UnitOfWork;

        ResultViewModel result = new ResultViewModel();

        public ProductController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepo = UnitOfWork.GetProductRepo();
            StoreRepo = UnitOfWork.GetStoreRepo();
        }

        [HttpGet("")]

       
        public  ResultViewModel Get()
        {

            result.Message = "All Products";
            result.Data = ProductRepo.Get().Select(p => p.ToViewModel());
            return result;
        }

        [HttpGet("{id}")]
        public ResultViewModel GetProductByID(int id)
        {
            result.Message = " Product By ID";
           
            result.Data = ProductRepo.GetByID(id).ToViewModel();

            return result;

        }
        


    }
}
