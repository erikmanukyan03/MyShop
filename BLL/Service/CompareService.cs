using BLL.Extention;
using BLL.Service.Interfaces;
using BLL.ViewModels;
using Domain;
using Domain.Entities;
using Domain.Migrations;
using Domain.Repository;
using Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public Dictionary<string, List<string>> GetAll(string cookieId)
		{
			var list = _context.Compares.GetAll(cookieId);
            var prods = _context.Products.GetByIds(list);
			var newlist= new List<ProductCompareVM>();
			foreach (var item in prods)
			{
				var prod = new ProductCompareVM
				{
					Id = item.Id,
					Slug = item.Slug,
					ImageFile = item.MainImage,
					Memory = item.Memory,
					Price = (double)(item.Discount > 0 ? item.Price - item.Price * item.Discount / 100 : item.Price),
					Title = item.Title,
					PAVVMs = item.PAVs.Select(pa => new Atribute_ValueVM
					{
						Id = pa.Id,
						Name = pa.ProductAttribute.Name,
						Value = pa.Value,
					}).ToList(),
				};
				newlist.Add(prod);
            }
			  var dict = new Dictionary<string, List<string>>();
            foreach (var item in newlist)
            {
                foreach (var item1 in item.GetType().GetProperties())
                {
                    if (item1.Name == "Id")
                    {
                        continue;
                    }
                    else if (item1.Name == "PAVVMs")
                    {
                        foreach (var item2 in item.PAVVMs)
                        {
                            if (!dict.ContainsKey(item2.Name))
                            {
                            dict[item2.Name] = new List<string> { item2.Value};
                            }
                            else
                            {
                                dict[item2.Name].Add(item2.Value);
                            }
                        }
                    }
                    else
                    {
                        if (!dict.ContainsKey(item1.Name))
                        {
							if (item1.Name == "Title")
							{
                                var linkHtml = $"<a asp-area=\"\" asp-controller=\"Product\" asp-acton=\"Details\" aso-route-slug=\"{item.Slug}\"\">{item1.GetValue(item)?.ToString()}</a>";
                                dict[item1.Name] = new List<string> { linkHtml };
                            }
							else if (item1.Name == "ImageFile")
							{
                                dict[item1.Name] = new List<string> { $"<img src=\"/assets/img/product/{item1.GetValue(item)?.ToString()}\" />" };
                            }
							else
							{
                            dict[item1.Name] = new List<string> { item1.GetValue(item)?.ToString() };
							}
                        }
                        else
                        {
                            if (item1.Name == "Title")
                            {
                                var linkHtml = $"<a asp-area=\"\" asp-controller=\"Product\" asp-acton=\"Details\" aso-route-slug=\"{item.Slug}\"\">{item1.GetValue(item)?.ToString()}</a>";
                                dict[item1.Name].Add(linkHtml);

                            }
                            else if (item1.Name == "ImageFile")
                            {
                                dict[item1.Name].Add($"<img src=\"/assets/img/product/{item1.GetValue(item)?.ToString()}\" />");
                            }
							else { 
                            dict[item1.Name].Add(item1.GetValue(item)?.ToString());
							}
                        }
                    }
                }
            }
			return dict;
		}
	}
}
