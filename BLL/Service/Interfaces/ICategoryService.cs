using BLL.ViewModels;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface ICategoryService
    {

        void Add(CategoryAddEditVM model);
        void Update(CategoryAddEditVM models);
        CategoryVM GetById(int Id);
        Task<List<CategoryName>> GetAll();
        CategoryAddEditVM GetForEdit(int Id);
        void Delete(int Id);
        public List<CategoryVM> GetAllForAdmin(bool IsDeleted);
		//Task<List<CategoryName>> CategoryNames();
        Task<List<CategoryForProduct>> GetForProducts();
        Task<List<ShortProductVM>> Filter(int? categoryId,FilterVM model);
        Task<Tuple<int?, int?>> MinMaxPrice(int categoryId);
    }
}
