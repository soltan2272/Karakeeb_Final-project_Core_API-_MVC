using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposotries
{
    public class UnitOfWork : IUnitOfWork
    {
        Project_Context Context;
        IGenericRepostory<Admin> AdminRepo;
        IGenericRepostory<Category> CategoryRepo;
        IGenericRepostory<Contact> ContactRepo;
        IGenericRepostory<Courier> CourierRepo;
        IGenericRepostory<Feedback> FeedbackRepo;
        IGenericRepostory<Offer> OfferRepo;
        IGenericRepostory<Order> OrderRepo;
        IGenericRepostory<Payment> PaymentRepo;
        IGenericRepostory<Product> ProductRepo;
        IGenericRepostory<Store> StoreRepo;
        IGenericRepostory<Supplier> SupplierRepo;
       // IGenericRepostory<User> UserRepo;
        public UnitOfWork(Project_Context context, IGenericRepostory<Admin> adminRepo,
                            IGenericRepostory<Category> categoryRepo, IGenericRepostory<Contact> contactRepo,
                            IGenericRepostory<Courier> courierRepo, IGenericRepostory<Feedback> feedbackRepo,
                            IGenericRepostory<Offer> offerRepo, IGenericRepostory<Order> orderRepo,
                            IGenericRepostory<Payment> paymentRepo, IGenericRepostory<Product> productRepo,
                            IGenericRepostory<Store> storeRepo, IGenericRepostory<Supplier> supplierRepo)
                           // IGenericRepostory<User> userRepo)
        {


            Context = context;
            AdminRepo = adminRepo;
            CategoryRepo = categoryRepo;
            ContactRepo = contactRepo;
            CourierRepo = courierRepo;
            FeedbackRepo = feedbackRepo;
            OfferRepo = offerRepo;
            OrderRepo = orderRepo;
            PaymentRepo = paymentRepo;
            ProductRepo = productRepo;
            StoreRepo = storeRepo;
            SupplierRepo = supplierRepo;
           // UserRepo = userRepo;
        }

        public IGenericRepostory<Admin> GetAdminRepo()
        {
            return AdminRepo;

        }
        public IGenericRepostory<Category> GetCategoryRepo()
        {
            return CategoryRepo;

        }
        public IGenericRepostory<Contact> GetContactRepo()
        {
            return ContactRepo;

        }
        public IGenericRepostory<Courier> GetCourierRepo()
        {
            return CourierRepo;

        }
        public IGenericRepostory<Feedback> GetFeedbackRepo()
        {
            return FeedbackRepo;

        }
        public IGenericRepostory<Offer> GetOfferRepo()
        {
            return OfferRepo;

        }
        public IGenericRepostory<Order> GetOrderRepo()
        {
            return OrderRepo;

        }
        public IGenericRepostory<Payment> GetPaymentRepo()
        {
            return PaymentRepo;

        }
        public IGenericRepostory<Product> GetProductRepo()
        {
            return ProductRepo;

        }
        public IGenericRepostory<Store> GetStoreRepo()
        {
            return StoreRepo;

        }
        public IGenericRepostory<Supplier> GetSupplierRepo()
        {
            return SupplierRepo;

        }
     /*   public IGenericRepostory<User> GetUserRepo()
        {
            return UserRepo;

        }*/

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }
    }
}
