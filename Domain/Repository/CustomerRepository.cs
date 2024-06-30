using Domain.Entities;
using Domain.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyShopDb _context;
        public CustomerRepository(MyShopDb context)
        {
            _context = context;
        }
        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }
        public void Delete(int Id)
        {
            var entity = _context.Customers.FirstOrDefault(c => c.Id == Id);
            entity.IsDeleted = true;
        }

        public Customer GetById(int Id)
        {
           return  _context.Customers.FirstOrDefault(c=>c.Id==Id);

        }
    }
}
