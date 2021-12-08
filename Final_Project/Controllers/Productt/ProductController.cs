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
        IUserRepository UserRepository;
        IGenericRepostory<Store> StoreRepo;
        IUnitOfWork UnitOfWork;

        ResultViewModel result = new ResultViewModel();

        public ProductController(IUserRepository userRepository,IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepo = UnitOfWork.GetProductRepo();
            StoreRepo = UnitOfWork.GetStoreRepo();
            CategoryRepo = unitOfWork.GetCategoryRepo();
            UserRepository = userRepository;

        }

        [HttpGet("userProducts")]
        public  ResultViewModel GetforUser()
        {

            result.Message = "All Products";
            result.Data = ProductRepo.Get().Select(p => p.ToViewModel());
            return result;
        }
        [HttpGet("AdminProducts/{p}")]
        public ResultViewModel GetforAdmin(int p)
        {
            result.Message = "All Products";
            result.Data = ProductRepo.Get().OrderByDescending(p => p.ID).Skip(p * 7).Take(7);

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
            StoreProduct sp = new StoreProduct();
            result.Message = "Add Product";

            var Cat = CategoryRepo.Get().Where(c => c.ID == product.CurrentCategoryID).FirstOrDefault();
            if (Cat != null)
            {
                Cat.Products.Add(product);
            }



            var store = StoreRepo.Get().
                Where(s => s.StoresProducts.Where(sp => sp.Product_ID == product.ID).FirstOrDefault() != null).FirstOrDefault();
            if (store == null)
            {
                sp.Product_ID = product.ID;
                product.StoresProducts.Add(sp);
            }

                ID = pro.ID,
                Name = pro.Name,
                //Image = pro.Image,
                Rate = pro.Rate,
                Description = pro.Description,
                Price = pro.Price,
                CurrentCategoryID = pro.CurrentCategoryID,
                CurrentSupplierID = pro.CurrentSupplierID,
            };
            //product.supplier.
            //var store = StoreRepo.Get().FirstOrDefault(s => s.SupllierStores
            //                .FirstOrDefault(ss => s.ID == product.CurrentSupplierID) != null);
            //if (store != null)
            //{
            //    store.StoresProducts.FirstOrDefault(
            //}

            ProductRepo.Add(product);
            UnitOfWork.Save();
            result.Data = product;

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
            product.Image = pro.Image;

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

     /*   [HttpGet("stores")]


        public ResultViewModel GetStores()
        {
            result.Message = "All stores";
            result.Data = StoreRepo.Get();
            return result;
        }

        [HttpGet("storesById/{id}")]
        public ResultViewModel storesById(int id)
        {
            Store store = StoreRepo.GetByID(id);
            if (store == null)
            {
                result.ISuccessed = false;
                return result;
            }
            result.Message = " Store By ID";
            result.Data = StoreRepo.GetByID(id);
            return result;
        }

        [HttpPost("addStore")]
       /* [HttpPost("addStore")]
        public ResultViewModel addStore(StoreViewModel sto)
        {
            result.Message = "Add Store";
            var store = new Store();
            store.Name = sto.Name;
            store.Address = sto.Address;
            store.Phone = sto.Phone;
            

            StoreRepo.Add(store);
            UnitOfWork.Save();
            result.Data = store;

            return result;

        }*/
     /*   [HttpPut("editStore")]
        public ResultViewModel editStore(int id, StoreViewModel sto)
        {
            var store = StoreRepo.GetByID(id);
            result.Data = ProductRepo.GetByID(id).ToViewModel();

            store.Name = sto.Name;
            store.Address = sto.Address;
            store.Phone = sto.Phone;

            if (store == null)
            {
                result.Message = "NotFound Store";
            }
            result.Data = store;
            StoreRepo.Update(store);
            UnitOfWork.Save();
            return result;
        }*/

        
      /*  [HttpDelete("deleteStore/{id}")]
        public ResultViewModel deleteStore(int id)
        {
            result.Data = StoreRepo.GetByID(id);
            StoreRepo.Remove(StoreRepo.GetByID(id));
            UnitOfWork.Save();
            result.Message = "Store Deleted";
            return result;
        }*/



    }
}
