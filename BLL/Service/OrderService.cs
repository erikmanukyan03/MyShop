using BLL.Extention;
using BLL.ViewModels;
using BLL.ViewModels.Order;
using Domain;
using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class OrderService : IOrderService
    {
        private readonly MyShopDb _context;
        private readonly IOrderRepository _orderRepository;
        private readonly IUOW _uow;
        public OrderService(IUOW uow, IOrderRepository orderRepository, MyShopDb context)
        {
            _context = context;
            _orderRepository = orderRepository;
            _uow = uow;
        }
        public int Add(OrderVM model)
        {
            var entity = new Order()
            {
                CookieId = model.CookieId,
                Customer = new Customer
                {
                    Address = model.Customer.Address,
                    Email = model.Customer.Email,
                    Name = model.Customer.Name,
                    PhoneNumber = model.Customer.PhoneNumber,
                    RoleType = model.Customer.CustomerType,
                },
                OrderDate = DateTime.Now,
            };
            _orderRepository.Add(entity);
            _uow.Save();
            return entity.Id;
        }

        public List<OrderVM> GetAll(bool isDeleted)
        {
            return _context.Orders.GetAll(isDeleted).Select(o => new OrderVM
            {
                Id = o.Id,
                CookieId = o.CookieId,
                Customer = new CustomerVM
                {
                    Id = o.Customer.Id,
                    Address = o.Customer.Address,
                    Email = o.Customer.Email,
                    //CustomerType= o.Customer.RoleType,
                    //IsDeleted= o.Customer.IsDeleted,
                    //Message= o.Customer.Message,
                    Name = o.Customer.Name,
                    PhoneNumber = o.Customer.PhoneNumber,
                },
                OrderDate = o.OrderDate,
                IsDeleted = o.IsDeleted,
            }).ToList();
        }

        public List<OrderVM> GetByCookie(string CookieId)
        {
            return _context.Orders.GetByCookie(CookieId).Select(o => new OrderVM
            {
                Id = o.Id,
                CookieId = o.CookieId,
                Customer = new CustomerVM
                {
                    Id = o.Customer.Id,
                    Address = o.Customer.Address,
                    Email = o.Customer.Email,
                    //CustomerType= o.Customer.RoleType,
                    //IsDeleted= o.Customer.IsDeleted,
                    //Message= o.Customer.Message,
                    Name = o.Customer.Name,
                    PhoneNumber = o.Customer.PhoneNumber,
                },
                OrderDate = o.OrderDate,
                IsDeleted = o.IsDeleted,
            }).ToList();
        }

        public OrderVM GetById(int Id)
        {
            var entity = _context.Orders.GetById(Id);
            return new OrderVM
            {
                Id = entity.Id,
                CookieId = entity.CookieId,
                Customer = new CustomerVM
                {
                    Id = entity.Customer.Id,
                    Address = entity.Customer.Address,
                    Email = entity.Customer.Email,
                    //CustomerType= entity.Customer.RoleType,
                    //IsDeleted= entity.Customer.IsDeleted,
                    //Message= entity.Customer.Message,
                    Name = entity.Customer.Name,
                    PhoneNumber = entity.Customer.PhoneNumber,
                },
                OrderDate = entity.OrderDate,
                IsDeleted = entity.IsDeleted,
            };
        }

        public EmailOrder GetEmailOrder(int Id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == Id);
            var carts = _context.Carts.Where(c => c.CookieId == order.CookieId).ToList();
            var model = new EmailOrder
            {
                Details = new OrderVM
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    CookieId = order.CookieId,
                    Customer = new CustomerVM
                    {
                        Address = order.Customer.Address,
                        Email = order.Customer.Email,
                        Name = order.Customer.Name,
                        PhoneNumber = order.Customer.PhoneNumber,
                    },
                },
                Carts = carts.Select(c => new CartVM
                {
                    Count = c.Count,
                    Price = c.Price,
                    Product = new ProductVM
                    {
                        Description = c.Product.Description,
                        Discount = c.Product.Discount,
                        ShortDescription = c.Product.ShortDescription,
                        Title = c.Product.Title,
                        Price = c.Product.Price,
                        Image = c.Product.Image,
                    },
                }).ToList(),
            };
            return model;
        }
    }
}
