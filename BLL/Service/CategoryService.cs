using BLL.Extention;
using BLL.ViewModels;
using Domain;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUOW _uow;
        private readonly MyShopDb _context;
        private readonly IMemoryCache _cache;
        public CategoryService(ICategoryRepository categoryRepository, IUOW uow, MyShopDb context, IMemoryCache memoryCache)
        {
            _categoryRepository = categoryRepository;
            _uow = uow;
            _context = context;
            _cache = memoryCache;
        }
        public void Add(CategoryAddEditVM model)
        {
            var entity = new Category
            {
                Name = model.Name,
                Description = model.Description,
            };
            _categoryRepository.Add(entity);
            _uow.Save();
            _cache.Remove("Categories");
            _cache.Remove("ShortCategories");
        }

        public void Update(CategoryAddEditVM model)
        {
            var entity = new Category
            {
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                Name = model.Name,
                Description = model.Description,
            };
            _categoryRepository.Update(entity);
            _uow.Save();
            _cache.Remove("Categories");
            _cache.Remove("ShortCategories");
        }
        public CategoryVM GetById(int Id)
        {
            var entity = _context.Categories.GetById(Id);
            return new CategoryVM
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name,
                //MetaDescription=entity.MetaDescription,
                //PageTitle=entity.PageTitle,
                //Slug=entity.Slug,
                Products = entity.Products.Select(p => new ShortProductVM
                {
                    MetaDescription=p.MetaDescription,
                    PageTitle=p.PageTitle,
                    Slug=p.Slug,
                    Id = p.Id,
                    Title = p.Title,
                }).ToList(),
            };
        }
        public async Task<List<CategoryVM>> GetAll(bool IsDeleted)
        {
            var categories = new List<CategoryVM>();
            if (_cache.TryGetValue("Categories", out categories))
            {
                return categories;
            }
            var list = await _context.Categories.GetAll(IsDeleted);
            categories = list.Select(c => new CategoryVM
            {
                Id = c.Id,
                Description = c.Description,
                Name = c.Name,
                //MetaDescription = c.MetaDescription,
                //PageTitle = c.PageTitle,
                //Slug = c.Slug,
                Products = c.Products.Select(p => new ShortProductVM
                {
                    Id = p.Id,
                    Title = p.Title,
                }).ToList(),
            }).ToList();
            _cache.Set("Categories", categories);
            return categories;
        }

        public CategoryAddEditVM GetForEdit(int Id)
        {
            var entity = _context.Categories.GetById(Id);
            return new CategoryAddEditVM
            {
                IsDeleted = entity.IsDeleted,
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name,
                //MetaDescription = entity.MetaDescription,
                //PageTitle = entity.PageTitle,
                //Slug = entity.Slug,
            };
        }
        public void Delete(int Id)
        {
            _categoryRepository.Delete(Id);
            _uow.Save();
            _cache.Remove("Categories");
            _cache.Remove("ShortCategories");
        }

        public async Task<List<CategoryForProduct>> GetForProducts()
        {
            var categories = new List<CategoryForProduct>();
            if (_cache.TryGetValue("ShortCategories", out categories))
            {
                return categories;
            }
            var list = await _context.Categories.GetForProducts();
            categories = list.Select(c => new CategoryForProduct
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new ShortProductVM
                {
                    Id = p.Id,
                    Title = p.Title,
                    Discount = p.Discount,
                    Memory=p.Memory,
                    ProdColor=p.ProdColor,
                    ShortDescription = p.ShortDescription,
                    CategoryId = c.Id,
                    Price = p.Price,
                }).ToList(),
            }).ToList();
            _cache.Set("ShortCategories", categories);
            return categories;
        }

        public async Task<List<CategoryName>> CategoryNames()
        {
            var list = await _context.Categories.GetCategoryNames();
            var newlist = list.Select(c => new CategoryName
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
            return newlist;
        }
        public async Task<List<ShortProductVM>> Filter(int? categoryId,FilterVM model)
        {
            var list = await _context.Products.GetAll(categoryId,false);
            var filteredProducts = list.Where(p =>
        (model.RAM == null || p.PAVs.Any(av => av.ProductAttribute.Name == "RAM" && model.RAM.Contains(av.Value))) &&
        (model.ScreenSize == null || p.PAVs.Any(av => av.ProductAttribute.Name == "ScreenSize" && model.ScreenSize.Contains(av.Value))) &&
        (model.Memory == null || model.Memory.Contains($"{p.Memory}GB")) &&
        (model.Color==null || model.Color.Contains($"{p.ProdColor}"))&&
        ((p.Discount==0 && p.Price>model.MinPrice&&p.Price<=model.MaxPrice)|| (p.Discount >= 0 && p.Price - p.Price * p.Discount / 100 >= model.MinPrice && p.Price - p.Price * p.Discount / 100 <= model.MaxPrice))).Select(p => new ShortProductVM
        {
            Id = p.Id,
            Title = p.Title,
            ShortDescription = p.ShortDescription,
            Price = p.Price,
            Discount = p.Discount,
            CategoryId = p.CategoryId,
            ProdColor=p.ProdColor,
            Memory=p.Memory,
            Slug=p.Slug,
            MetaDescription=p.MetaDescription,
            PageTitle=p.PageTitle,
            PAVVMs = p.PAVs.Select(av => new Atribute_ValueVM
            {
                Id = av.Id,
                Name = av.ProductAttribute.Name,
                Value = av.Value,
            }).ToList(),
            ImageFile = p.Image,
            FinalPrice = (double)(p.Discount > 0 ? p.Price - p.Price * p.Discount / 100 : p.Price),
        }).ToList();
            return filteredProducts;
        }
        public async Task<Tuple<int?, int?>> MinMaxPrice(int categoryId)
        {
            return await _context.Products.MinMaxPrice(categoryId);
        }
    }
}