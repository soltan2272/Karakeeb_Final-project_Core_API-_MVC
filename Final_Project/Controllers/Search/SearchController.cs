using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("api/[controller]")]

    public class SearchController : ControllerBase
    {
        IGenericRepostory<Product> ProductRepo;
        IUnitOfWork UnitOfWork;

        ResultViewModel result = new ResultViewModel();
        public SearchController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepo = UnitOfWork.GetProductRepo();
        }
        [HttpGet("{Name}")]
        public ResultViewModel Name(string Name)
        {

            result.Message = "All Products have Name: " + Name;
            result.Data = ProductRepo.Get().Where(p => p.Name.Contains(Name)).Select(p => p.ToViewModel());
            return result;
        }

        [HttpGet("{price}")]
        public ResultViewModel PriceLessThan(int price)
        {


            result.Message = "Products Less Than "+price;
            result.Data = ProductRepo.Get().Where(p => p.Price<=price).Select(p => p.ToViewModel());
            return result;

        }

        [HttpGet("{price}")]
        public ResultViewModel PriceMoreThan(int price)
        {

            result.Message = "Products Less Than " + price;
            result.Data = ProductRepo.Get().Where(p => p.Price >= price).Select(p => p.ToViewModel());
            return result;

        }

        [HttpGet("{Rate}")]
        public ResultViewModel Rate(int Rate)
        {
            if(Rate<=5&& Rate >= 0)
            {
                result.Message = "Products Where Rate = " + Rate;
                result.Data = ProductRepo.Get().Where(p => p.Rate == Rate).Select(p => p.ToViewModel());

            }
            else
            {
                result.ISuccessed = false;
                result.Data = "error Rate not valied ";

            }
            return result;

        }

        [HttpGet("{Category}")]
        public ResultViewModel Category(int Category)
        {

            result.Message = "Products By Category Name ";
            result.Data = ProductRepo.Get().Where(p => p.CurrentCategoryID == Category).Select(p => p.ToViewModel());
            return result;

        }
        [HttpGet("{Seller}")]
        public ResultViewModel Seller(int Seller)
        {

            result.Message = "Products By Seller Name ";
            result.Data = ProductRepo.Get().Where(p => p.CurrentSupplierID == Seller).Select(p => p.ToViewModel());
            return result;

        }
    }
}
