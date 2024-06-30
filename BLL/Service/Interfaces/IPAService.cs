using BLL.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IPAService
    {
        int Add(PAVM model);
        void Update(PAVM model);
        void Delete(int Id);
        PAVM GetById(int Id);
        List<PAVM> GetByProdId(int Id);
        List<PAVM> GetAll();
    }
}
