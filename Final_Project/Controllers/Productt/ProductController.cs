using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ViewModels;
using Models.Models;

namespace Final_Project.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        IGenericRepostory<Product> ProductRepo;
        IGenericRepostory<Category> CategoryRepo;
        IGenericRepostory<Store> StoreRepo;
        IGenericRepostory<Images> ImageRepo;
        IUnitOfWork UnitOfWork;

        ResultViewModel result = new ResultViewModel();

        public ProductController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepo = UnitOfWork.GetProductRepo();
            StoreRepo = UnitOfWork.GetStoreRepo();
            CategoryRepo = UnitOfWork.GetCategoryRepo();
            ImageRepo = UnitOfWork.GetImagesRepo();
        }

        [HttpGet("userProducts")]
        public ResultViewModel GetforUser()
        {

            result.Message = "All Products";
            result.Data = ProductRepo.Get().Select(p => p.ToViewModel());
            return result;
        }
        [HttpGet("AdminProducts")]


        public ResultViewModel GetforAdmin()
        {
            result.Message = "All Products";
            result.Data = ProductRepo.Get();
            return result;
        }


        [HttpGet("{id}")]
        public ResultViewModel GetProductByID(int id)
        {
            Product product = ProductRepo.GetByID(id);
            if (product == null)
            {
                result.ISuccessed = false;
                return result;

            }

            result.Message = " Product By ID";
            result.Data = ProductRepo.GetByID(id).ToViewModel();



            return result;

        }

        [HttpGet("AdminProduct/{id}")]
        public ResultViewModel GetProductByIDForAdmin(int id)
        {
            Product product = ProductRepo.GetByID(id);
            if (product == null)
            {
                result.ISuccessed = false;
                return result;

            }
            result.Message = " Product By ID";
            result.Data = ProductRepo.GetByID(id);
            return result;

        }

        [HttpGet("GetProductBySupplierID/{id}")]
        public ResultViewModel GetProductBySupplierID(int id)
        {
            Product products = ProductRepo.Get().Where(s => s.CurrentSupplierID == id).FirstOrDefault();
            if (products == null)
            {
                result.ISuccessed = false;
                return result;
            }
            result.Message = " Product By Supplier ID";
            result.Data = products;
            return result;
        }




        [HttpPost("addProduct")]
        public ResultViewModel addProduct(Product product)
        {
            //StoreProduct sp = new StoreProduct();
            var res = product;
            result.Message = "Add Product";


            Category Cat = CategoryRepo.Get().Where(c => c.ID == product.CurrentCategoryID).FirstOrDefault();
            if (Cat != null)
            {
                product.category = Cat;
            }

            ProductRepo.Add(product);
            UnitOfWork.Save();
            result.Data = product;

            return result;
        }
        [HttpPost("addimages")]
        public ResultViewModel addimages(Images image)
        {
            var res = image;
            result.Message = "Add Images for Product";

            Product prod = ProductRepo.Get().Where(p => p.ID == image.CurrentProductID).FirstOrDefault();
            if (prod != null)
            {
                image.product = prod;
            }

            ImageRepo.Add(image);
            UnitOfWork.Save();
            result.Data = image;

            return result;
        }

        [HttpPut("editProduct")]
        public ResultViewModel editProduct(int id, InsertProductViewModel pro)
        {
            //if (id == null)
            //{
            //    result.Message = "Not Found Product";
            //}
            var product = ProductRepo.GetByID(id);
            product.ID = pro.ID;
            product.Name = pro.Name;
            product.Quantity = pro.Quantity;
         

           // product.Image = pro.Image;
            product.Rate = pro.Rate;
            product.Description = pro.Description;
            product.Price = pro.Price;
            product.CurrentCategoryID = pro.CurrentCategoryID;
            product.CurrentSupplierID = pro.CurrentSupplierID;


            if (product == null)
            {
                result.Message = "NotFound Product";
            }
            ProductRepo.Update(product);
            UnitOfWork.Save();
            return result;
        }
        [HttpDelete("Delete/{id}")]
        public ResultViewModel deleteProduct(int id)
        {
            result.Message = "Deleted Product";

            result.Data = ProductRepo.GetByID(id);
            ProductRepo.Remove(ProductRepo.GetByID(id));
            UnitOfWork.Save();
            return result;
        }

        [HttpGet("stores")]


        public ResultViewModel GetStores()
        {
            result.Message = "All stores";
            result.Data = StoreRepo.Get();
            return result;
        }

        //[HttpGet("storesById/{id}")]
        //public ResultViewModel storesById(int id)
        //{
        //    Store store = StoreRepo.GetByID(id);
        //    if (store == null)
        //    {
        //        result.ISuccessed = false;
        //        return result;
        //    }
        //    result.Message = " Store By ID";
        //    result.Data = StoreRepo.GetByID(id);
        //    return result;
        //}

        //[HttpPost("addStore")]
        //public ResultViewModel addStore(StoreViewModel sto)
        //{
        //    result.Message = "Add Store";
        //    var store = new Store();
        //    store.Name = sto.Name;
        //    store.Address = sto.Address;
        //    store.Phone = sto.Phone;


        //    StoreRepo.Add(store);
        //    UnitOfWork.Save();
        //    result.Data = store;

        //    return result;

        //}
        //[HttpPut("editStore")]
        //public ResultViewModel editStore(int id, StoreViewModel sto)
        //{
        //    var store = StoreRepo.GetByID(id);
        //    result.Data = ProductRepo.GetByID(id).ToViewModel();

        //    store.Name = sto.Name;
        //    store.Address = sto.Address;
        //    store.Phone = sto.Phone;

        //    if (store == null)
        //    {
        //        result.Message = "NotFound Store";
        //    }
        //    result.Data = store;
        //    StoreRepo.Update(store);
        //    UnitOfWork.Save();
        //    return result;
        //}


        //[HttpDelete("deleteStore/{id}")]
        //public ResultViewModel deleteStore(int id)
        //{
        //    result.Data = StoreRepo.GetByID(id);
        //    StoreRepo.Remove(StoreRepo.GetByID(id));
        //    UnitOfWork.Save();
        //    result.Message = "Store Deleted";
        //    return result;
        //}


        [HttpGet("category")]
        public ResultViewModel Getcategory()
        {

            result.Message = "All category";
            result.Data = CategoryRepo.Get();
            return result;
        }


       }
    }
     