using BLL.Extention;
using BLL.ViewModels;
using Domain;
using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Interfaces;

namespace BLL.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly MyShopDb _context;
        private readonly IUOW _uow;
        public CartService(ICartRepository cartRepository, MyShopDb context, IUOW uow)
        {
            _cartRepository = cartRepository;
            _context = context;
            _uow = uow;
        }
        public void Add(CartVM model)
        {
            var entity = new Cart()
            {
                CookieId = model.CookieId,
                Count = model.Count,
                IsDeleted = model.IsDeleted,
                Price = model.Price,
                ProductId = model.ProductId,
            };
            _cartRepository.Add(entity);
            _uow.Save();

        }
        public void Delete(int Id) 
        {
            _cartRepository.Delete(Id);
            _uow.Save();
        }
        public void DeleteAll(string cookieId)
        {
            _cartRepository.DeleteAll(cookieId);
            _uow.Save();
        }
        public List<CartVM> GetAll(bool isDeleted)
        {   
            return _context.Carts.GetAll(isDeleted).Select(c => new CartVM
            {
                Id = c.Id,
                CookieId = c.CookieId,
                Count = c.Count,
                IsDeleted = c.IsDeleted,
                Price = c.Price,
                Product = new ShortProductVM
                {
                    Title = c.Product.Title,
                                       Discount = c.Product.Discount,
                    Id = c.Product.Id,
                    ImageFile = c.Product.MainImage,
                    Price = c.Product.Price,
                    ShortDescription = c.Product.ShortDescription,
                },
            }).ToList();
        }
        public int ProdsCount(string cookieId)
        {
            return _context.Carts.ProdsCount(cookieId);
        }
        public void Update(CartVM model)
        {
            var entity = new Cart()
            {
                Id = model.Id,
                CookieId = model.CookieId,
                Count = model.Count,
                IsDeleted = model.IsDeleted,
                Price = model.Price,
                ProductId = model.ProductId,
            };
            _cartRepository.Update(entity);
            _uow.Save();
        }
        public List<CartVM> GetByCookie(string cookieId)
        {
            return _context.Carts.GetByCookie(cookieId).Select(c => new CartVM
            {
                Id = c.Id,
                CookieId = c.CookieId,
                Count = c.Count,
                IsDeleted = c.IsDeleted,
                Price = c.Price,
                ProductId=c.ProductId,
                Product = new ShortProductVM
                {
                    Id = c.Product.Id,
                    Title = c.Product.Title,
                    ImageFile = c.Product.MainImage,
                    Slug= c.Product.Slug,
                    Memory= c.Product.Memory,
                    ProdColor= c.Product.ProdColor,
                },
            }).ToList();
        }
        public List<CartVM> GetHistory(string cookieId)
        {
            return _context.Carts.GetHistory(cookieId).Select(c => new CartVM
            {
                Id = c.Id,
                CookieId = c.CookieId,
                Count = c.Count,
                IsDeleted = c.IsDeleted,
                Price = c.Price,
                ProductId = c.ProductId,
                Product = new ShortProductVM
                {
                    Title = c.Product.Title,
                    //CategoryId = c.Product.CategoryId,
                    //Description = c.Product.Description,
                    //Discount = c.Product.Discount,
                    Id = c.Product.Id,
                    ImageFile = c.Product.MainImage,
                    //IsDeleted = c.Product.IsDeleted,
                    ProdColor = c.Product.ProdColor,
                    Memory = c.Product.Memory,
                    Slug = c.Product.Slug,
                    //IsHot = c.Product.IsHot,
                    //Price = c.Product.Price,
                    //ShortDescription = c.Product.ShortDescription,
                },
            }).ToList();
        }
        public CartVM GetForEdit(int cartId)
        {
            var entity=_context.Carts.GetForEdit(cartId);
            return new CartVM
            {
                CookieId = entity.CookieId,
                IsDeleted = entity.IsDeleted,
                Count = entity.Count,
                Id = cartId,
                Price = entity.Price,
                ProductId = entity.ProductId,
            };
        }
    }
}