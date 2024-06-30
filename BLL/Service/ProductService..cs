using BLL.Extention;
using BLL.ViewModels;
using BLL.ViewModels.PrPav;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPrPAV _prPAVRepository;
        private readonly IUOW _uow;
        private readonly MyShopDb _context;
        private readonly IMemoryCache _cache;

        public ProductService(IProductRepository productRepository, IUOW uow, MyShopDb context, IMemoryCache cache, IPrPAV prPAVRepository)
        {
            _productRepository = productRepository;
            _context = context;
            _uow = uow;
            _cache = cache;
            _prPAVRepository = prPAVRepository;
        }
        public void Add(ProductVM model)
        {
            var entity = new Product
            {
                Title = model.Title,
                ShortDescription = model.ShortDescription,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Discount = model.Discount,
                Memory = model.Memory,
                ProdColor= model.ProdColor,
                Image = model.Image,
                Price = model.Price,
                MetaDescription= model.MetaDescription,
                Slug= model.Slug,
                PageTitle= model.PageTitle,
                IsHot = model.IsHot,
            };
            _productRepository.Add(entity);
            _uow.Save();
            _cache.Remove("Products");
        }

        public void Delete(int Id)
        {
            _productRepository.Delete(Id);
            _uow.Save();
            _cache.Remove("Products");
        }

        public void Update(ProductVM model)
        {
            var entity = new Product
            {
                Id = model.Id,
                Title = model.Title,
                ShortDescription = model.ShortDescription,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Discount = model.Discount,
                Image = model.Image,
                Memory = model.Memory,
                ProdColor = model.ProdColor,
                Price = model.Price,
                MetaDescription = model.MetaDescription,
                Slug = model.Slug,
                PageTitle = model.PageTitle,
                IsDeleted = model.IsDeleted,
                IsHot = model.IsHot,
            };
            _productRepository.Update(entity);
            _uow.Save();
            _cache.Remove("Products");
        }
        public ProductVM GetById(int Id)
        {
            var entity = _context.Products.GetById(Id);
            return new ProductVM
            {
                Id = entity.Id,
                Title = entity.Title,
                ShortDescription = entity.ShortDescription,
                CategoryId = entity.CategoryId,
                Description = entity.Description,
                Discount = entity.Discount,
                Image = entity.Image,
                Memory = entity.Memory,
                ProdColor = entity.ProdColor,
                Price = entity.Price,
                IsDeleted = entity.IsDeleted,
                IsHot = entity.IsHot,
                MetaDescription = entity.MetaDescription,
                Slug = entity.Slug,
                PageTitle = entity.PageTitle,
                PAVVMs = entity.PAVs.Select(av => new Atribute_ValueVM
                {
                    Id = av.Id,
                    Name = av.ProductAttribute.Name,
                    Value = av.Value,
                }).ToList(),
                FinalPrice = (double)(entity.Discount > 0 ? entity.Price - entity.Price * entity.Discount / 100 : entity.Price),
            };
        }

        public async Task<List<ShortProductVM>> GetAll(int? categoryId, bool IsDeleted)
        {
            var list = new List<ShortProductVM>();
            if (_cache.TryGetValue("Products", out list))
            {
                return list;
            }

            var prods = await _context.Products.GetAll(categoryId,IsDeleted);
            list = prods.Select(p => new ShortProductVM
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Discount = p.Discount,
                Price = p.Price,
                PAVVMs=p.PAVs.Select(av => new Atribute_ValueVM
                {
                    Id = av.Id,
                    Name = av.ProductAttribute.Name,
                    Value = av.Value,
                }).ToList(),
                ImageFile = p.Image,
                Slug=p.Slug,
                FinalPrice = (double)(p.Discount > 0 ? p.Price - p.Price * p.Discount / 100 : p.Price),
                Memory = p.Memory,
                ProdColor = p.ProdColor,
                CategoryId = p.CategoryId,
            }).ToList();
            _cache.Set("Products", list);

            return list;
        }

        public async Task<List<ShortProductVM>> GetHots()
        {
            var list = new List<ShortProductVM>();
            if (_cache.TryGetValue("Hots", out list))
            {
                return list;
            }
            var hots = await _context.Products.GetHots();
            list = hots.Select(p => new ShortProductVM
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Discount = p.Discount,
                Price = p.Price,
                PAVVMs = p.PAVs.Select(av => new Atribute_ValueVM
                {
                    Id = av.Id,
                    Name = av.ProductAttribute.Name,
                    Value = av.Value,
                }).ToList(),
                ImageFile = p.Image,
                Memory = p.Memory,
                MetaDescription = p.MetaDescription,
                Slug = p.Slug,
                PageTitle = p.PageTitle,
                ProdColor = p.ProdColor,
                FinalPrice = (double)(p.Discount > 0 ? p.Price - p.Price * p.Discount / 100 : p.Price),
                CategoryId = p.CategoryId,
            }).ToList();
            _cache.Set("Hots", list);
            return list;
        }

        public void AddEditAttribute(int prodId, List<int> list, bool edit)
        {
            _prPAVRepository.AddEdit(prodId, list, edit);
            _uow.Save();
        }

        public async Task<List<IGrouping<string,Atribute_ValueVM>>> GetByCategory(int categoryId)
        {
            var pavs = await _context.Products.GetByCategory(categoryId);
            if (pavs.Count != 0)
            {
                var list = new List<Atribute_ValueVM>();
                foreach (var item in pavs)
                {
                    if (!list.Any(p => p.Id == item.Id))
                    {
                        var model = new Atribute_ValueVM()
                        {
                            Id = item.Id,
                            Name = item.ProductAttribute.Name,
                            Value = item.Value,
                        };
                        list.Add(model);
                    }
                }
                var attributes = list.GroupBy(p => p.Name).ToList();
                return attributes;
            }
            else
            {
                return new List<IGrouping<string, Atribute_ValueVM>>();
            }
        }
        public async Task<List<string>> GetMemories(int categoryId)
        {
           var list= await _context.Products.Where(p=>p.CategoryId==categoryId).Select(p => $"{p.Memory}GB").Distinct().ToListAsync();
            return list;
        }

        public async Task<List<ShortProductVM>> Search(List<ShortProductVM>? list, string[]? splited)
        {
            var newlist = new List<ShortProductVM>();
            foreach (var item in list)
            {
                int count = 0;
                foreach (var s in splited)
                {
                    if (item.Title.ToLower().Contains(s.ToLower()) || item.ProdColor.ToString().ToLower().Contains(s.ToLower()) || (item.Memory.ToString() + "GB").ToLower().Contains(s.ToLower()) || item.ShortDescription.ToLower().Contains(s.ToLower()) || item.PAVVMs.Any(p => p.Value.ToLower().Contains(s.ToLower())))
                    {
                        count++;
                    }
                }
                if (count == splited.Length)
                {
                    newlist.Add(item);
                }
            }
            return newlist;
        }
        public ProductVM GetBySlug(string slug)
        {
            var entity =  _context.Products.GetBySlug(slug);
            return new ProductVM
            {
                Id = entity.Id,
                Title = entity.Title,
                ShortDescription = entity.ShortDescription,
                CategoryId = entity.CategoryId,
                Description = entity.Description,
                Discount = entity.Discount,
                Image = entity.Image,
                Memory = entity.Memory,
                ProdColor = entity.ProdColor,
                Price = entity.Price,
                IsDeleted = entity.IsDeleted,
                IsHot = entity.IsHot,
                MetaDescription = entity.MetaDescription,
                Slug = entity.Slug,
                PageTitle = entity.PageTitle,
                PAVVMs = entity.PAVs.Select(av => new Atribute_ValueVM
                {
                    Id = av.Id,
                    Name = av.ProductAttribute.Name,
                    Value = av.Value,
                }).ToList(),
                FinalPrice = (double)(entity.Discount > 0 ? entity.Price - entity.Price * entity.Discount / 100 : entity.Price),
            };
        }

        public List<string> GetProductUrls()
        {
            var url = _context.Products.Where(p => !p.IsDeleted)
                 .Select(p => p.Slug)
                 .ToList();
            return url;
        }
    }
}