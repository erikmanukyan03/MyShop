using BLL.Extention;
using BLL.Service.Interfaces;
using BLL.ViewModels;
using Domain;
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
	public class CompareService : ICompareSevice
	{
		private readonly ICompareRepository _compareRepository;
		private readonly IUOW _uow;
		private readonly MyShopDb _context;
		public CompareService(ICompareRepository compareRepository, IUOW uow, MyShopDb context)
		{
			_compareRepository = compareRepository;
			_uow = uow;
			_context = context;
		}
		public void Add(int prodId, string cookieId, int categoryId)
		{
			if (!_context.Compares.IsCompared(cookieId, prodId))
			{
				var entity = new Compare
				{
					CookieId = cookieId,
					ProductId = prodId,
					CategoryId = categoryId,
				};
				_compareRepository.Add(entity);
				_uow.Save();
			}
		}

		public void Delete(int Id)
		{
			_compareRepository.Delete(Id);
			_uow.Save();
		}

		public void DeleteAll(string cookieId, int categoryId)
		{
			_compareRepository.DeleteAll(cookieId, categoryId);
			_uow.Save();
		}

		public List<CompareVM> GetAll(string cookieId)
		{
			var list = _context.Compares.GetAll(cookieId);
			var newlist = new List<CompareVM>();
			foreach (var item in list)
			{
				var prod = _context.Products.GetById(item.ProductId);
				var entity = new CompareVM
				{
					Id = item.Id,
					CategoryId = item.CategoryId,
					CookieId = item.CookieId,
					Product = new ProductCompareVM
					{
						Id = prod.Id,
						Discount = prod.Discount,
						Slug = prod.Slug,
						CategoryId = prod.CategoryId,
						ImageFile = prod.MainImage,
						IsHot = prod.IsHot,
						Memory = prod.Memory,
						Price = prod.Price,
						Title = prod.Title,
						PAVVMs = prod.PAVs.Select(pa => new Atribute_ValueVM
						{
							Id = pa.Id,
							Name = pa.ProductAttribute.Name,
							Value = pa.Value,
						}).ToList(),
						FinalPrice = (double)(prod.Discount > 0 ? prod.Price - prod.Price * prod.Discount / 100 : prod.Price),
					}
				};
				newlist.Add(entity);
			}
			return newlist;
		}
	}
}
