using BLL.Extention;
using BLL.ViewModels;
using Domain;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

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
                MainImage = model.MainImage,
                Price = model.Price,
                MetaDescription= model.MetaDescription,
                Slug= model.Slug,
                PageTitle= model.PageTitle,
                Vendor= model.Vendor,
                IsHot = model.IsHot,
				Images = model.Images.Select(pi => new ProductImage
				{
					ImageFile = pi.ImageFile,
					ProductId = pi.ProductId,
				}).ToList(),
			};
            _productRepository.Add(entity);
            _uow.Save();
            _cache.Remove("Products");
			_cache.Remove("Hots");

		}

		public void Delete(int Id)
        {
            _productRepository.Delete(Id);
            _uow.Save();
            _cache.Remove("Products");
            _cache.Remove("Hots");
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
                MainImage = model.MainImage,
                Memory = model.Memory,
                ProdColor = model.ProdColor,
                Vendor=model.Vendor,
                Price = model.Price,
                MetaDescription = model.MetaDescription,
                Slug = model.Slug,
                PageTitle = model.PageTitle,
                IsDeleted = model.IsDeleted,
                IsHot = model.IsHot,
				Images = model.Images.Select(pi => new ProductImage
				{
					ImageFile = pi.ImageFile,
					ProductId = pi.ProductId,
				}).ToList(),
			};
            _productRepository.Update(entity);
            _uow.Save();
            _cache.Remove("Products");
			_cache.Remove("Hots");

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
                MainImage = entity.MainImage,
                Memory = entity.Memory,
                ProdColor = entity.ProdColor,
                Price = entity.Price,
                IsDeleted = entity.IsDeleted,
                IsHot = entity.IsHot,
                MetaDescription = entity.MetaDescription,
                Vendor= entity.Vendor,
                Slug = entity.Slug,
                PageTitle = entity.PageTitle,
				Images = entity.Images.Select(pi => new ProductImageVM
				{
					ImageFile = pi.ImageFile,
					Id = pi.Id,
					ProductId = pi.Id,
				}).ToList(),
				PAVVMs = entity.PAVs.Select(av => new Atribute_ValueVM
                {
                    Id = av.Id,
                    Name = av.ProductAttribute.Name,
                    Value = av.Value,
                }).ToList(),
                FinalPrice = (double)(entity.Discount > 0 ? entity.Price - entity.Price * entity.Discount / 100 : entity.Price),
            };
        }
        public List<string> GetVendors(int categoryId)
        {
            return _context.Products.GetVendors(categoryId);
        }
        public async Task<List<ProductForSearch>> GetAll(int? categoryId, bool IsDeleted)
        {
            var list = new List<ProductForSearch>();
            if (_cache.TryGetValue("Products", out list))
            {
                return list;
            }

            var prods = await _context.Products.GetAll(categoryId,IsDeleted);
            list = prods.Select(p => new ProductForSearch
			{
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                PAVVMs = p.PAVs.Select(av => new Atribute_ValueVM
                {
                    Id = av.Id,
                    Name = av.ProductAttribute.Name,
                    Value = av.Value,
                }).ToList(),
                ImageFile = p.MainImage,
                Slug = p.Slug,
                Memory = p.Memory,
                ProdColor = p.ProdColor,
            }).ToList();
            _cache.Set("Products", list);

            return list;
        }
        public List<ShortProductVM> GetAllForAdmin(int? categoryId, bool IsDeleted)
        {
			var list=_context.Products.GetAllForAdmin(categoryId, IsDeleted);
            return list.Select(p=>new ShortProductVM 
            {
                Id=p.Id,
                ImageFile=p.MainImage,
                Discount = p.Discount,
				FinalPrice = (double)(p.Discount > 0 ? p.Price - p.Price * p.Discount / 100 : p.Price),
				Price = p.Price,
                ShortDescription=p.ShortDescription,
                Memory = p.Memory,
                ProdColor=p.ProdColor,
                
                Slug = p.Slug,
                Title = p.Title,
            }).ToList() ;
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
                Discount = p.Discount,
                Price = p.Price,
                ImageFile = p.MainImage,
                Memory = p.Memory,
                ShortDescription = p.ShortDescription,
                Slug = p.Slug,
                ProdColor = p.ProdColor,
                FinalPrice = (double)(p.Discount > 0 ? p.Price - p.Price * p.Discount / 100 : p.Price),
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

        public async Task<List<ProductForSearch>> Search(List<ProductForSearch>? list, string[]? splited)
        {
            var newlist = new List<ProductForSearch>();
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
                MainImage = entity.MainImage,
                Memory = entity.Memory,
                ProdColor = entity.ProdColor,
				Images = entity.Images.Select(pi => new ProductImageVM
				{
					ImageFile = pi.ImageFile,
					Id = pi.Id,
					ProductId = pi.ProductId,
				}).ToList(),
				Price = entity.Price,
                IsDeleted = entity.IsDeleted,
                IsHot = entity.IsHot,
                MetaDescription = entity.MetaDescription,
                Slug = entity.Slug,
                PageTitle = entity.PageTitle,
                Vendor=entity.Vendor,   
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
		public void DeleteImage(int ImageId)
		{
			_productRepository.DeleteImage(ImageId);
			_uow.Save();
		}

	}
}