using BLL.Service.Interfaces;
using BLL.ViewModels;
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
        public CompareService(ICompareRepository compareRepository,IUOW uow)
        {
            _compareRepository = compareRepository;
			_uow = uow;
        }
        public void Add(CompareVM model)
		{
			var entity = new Compare()
			{
				CategoryId = model.CategoryId,
				CookieId = model.CookieId,
			};
			_compareRepository.Add(entity);
			_uow.Save();
		}

		public void Delete(int Id)
		{
			throw new NotImplementedException();
		}

		public void DeleteAll(string cookieId, int categoryId)
		{
			throw new NotImplementedException();
		}

		public List<CompareVM> GetAll(string cookieId)
		{
			throw new NotImplementedException();
		}
	}
}
