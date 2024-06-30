using BLL.Service.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUOW _uow;
        public CustomerService(IUOW uow, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _uow = uow;
        }
        public int Add(CustomerVM model)
        {
            var entity = new Customer
            {
                Email = model.Email,
                Address = model.Address,
                Message = model.Message,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                RoleType = model.CustomerType,
            };
            _customerRepository.Add(entity);
            _uow.Save();
            return entity.Id;
        }

        public void Delete(int Id)
        {
            _customerRepository.Delete(Id);
            _uow.Save();
        }

        public CustomerVM GetById(int Id)
        {
            var entity = _customerRepository.GetById(Id);
            var model = new CustomerVM
            {
                Id = Id,
                Address = entity.Address,
                Name = entity.Name,
                IsDeleted = entity.IsDeleted,
                CustomerType = entity.RoleType,
                Email = entity.Email,
                Message = entity.Message,
                PhoneNumber = entity.PhoneNumber,
            };
            return model;
        }
    }
}
