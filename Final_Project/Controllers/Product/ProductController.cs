using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class ProductController:ControllerBase
    {
        IGenericRepostory<Product> ProductRepo;
        IUnitOfWork UnitOfWork;

        ResultViewModel result = new ResultViewModel();

        public ProductController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepo = UnitOfWork.GetProductRepo();

        }

        [HttpGet("")]

        public  ResultViewModel Get()
        {

            result.Message = "All Products";
            result.Data = ProductRepo.Get().Select(p=> new ProductViewModel() {
                ID=p.ID,
                Name=p.Name,
                Image=p.Image,
                Rate=p.Rate,
                Description=p.Description,
                Price=p.Price
            });
            return result;
        }

        [HttpGet("{id}")]
        public ResultViewModel GetProductByID(int id)
        {
            result.Message = " Product By ID";

            Product p = ProductRepo.GetByID(id);
            ProductViewModel productview = new ProductViewModel()
            {
                ID = p.ID,
                Name = p.Name,
                Image = p.Image,
                Rate = p.Rate,
                Description = p.Description,
                Price = p.Price
            };
            result.Data = productview;
            
            return result;

        }
    }
}
