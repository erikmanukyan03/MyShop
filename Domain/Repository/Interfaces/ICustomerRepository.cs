using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Interfaces
{
    public interface ICustomerRepository 
    {
        void Add(Customer customer);
        void Delete(int Id);
        Customer GetById(int Id);
    }
}
