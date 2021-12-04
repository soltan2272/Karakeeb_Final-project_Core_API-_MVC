using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SearchController:ControllerBase
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

        public ResultViewModel SerchByName(string Name)
        {

            result.Message = "All Products have Name: "+Name;
            result.Data = ProductRepo.Get(). Where(p=>p.Name.Contains(Name)).Select(p => new ProductViewModel()
            {
                ID = p.ID,
                Name = p.Name,
                Image = p.Image,
                Rate = p.Rate,
                Description = p.Description,
                Price = p.Price
            });
            return  result;
        }
        }
}
