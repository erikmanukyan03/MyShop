using Domain.Entities;
using Domain.Repository;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels;
using Microsoft.EntityFrameworkCore;
using BLL.ViewModels.Product;

namespace BLL.Service
{
    public interface IProductService
    {
        void Add(ProductVM model);
        void Delete(int Id);
        void Update(ProductVM model);
        ProductVM GetById(int Id);
        void AddEditAttribute(int prodId, List<int> list,bool edit);
        Task<List<ProductForSearch>> GetAll(int? categoryId,bool IsDeleted);
        Task<List<ShortProductVM>> GetHots();
        Task<List<IGrouping<string, Atribute_ValueVM>>> GetByCategory(int categoryId);
        Task<List<string>> GetMemories(int categoryId);
        Task<List<ProductForSearch>> Search(List<ProductForSearch>? list,string[]? splited);
        ProductVM GetBySlug(string slug);
        List<string> GetProductUrls();
        List<ShortProductVM> GetAllForAdmin(int? categoryId, bool IsDeleted);

	}
}
