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

        IGenericRepostory<Offer> OfferRepo;

        ResultViewModel result = new ResultViewModel();

        public ProductController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepo = UnitOfWork.GetProductRepo();
            StoreRepo = UnitOfWork.GetStoreRepo();
            OfferRepo = UnitOfWork.GetOfferRepo();
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


        [HttpPost("addoffer")]
        public ResultViewModel AddOffer(Offer offer)
        {
            result.Message = "Add Offer To Product";

            OfferRepo.Add(offer);
            UnitOfWork.Save();
            result.Data = offer;

            return result;

        }

        [HttpDelete("deleteoffer")]
        public ResultViewModel DeleteOffer(int id)
        {
            result.Message = " offer Deleted";
            result.Data = OfferRepo.GetByID(id);
            OfferRepo.Remove(new Offer{ID = id});
            UnitOfWork.Save();

            return result;

        }

        [HttpPut("editoffer")]
        public ResultViewModel EditOffer(Offer offer)
        {

            result.Message = "edit offer";
            result.Data = offer;
            OfferRepo.Update(offer);
            UnitOfWork.Save();
            return result;
        }

        }
    }
