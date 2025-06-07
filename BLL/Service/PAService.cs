using BLL.Extention;
using BLL.Service.Interfaces;
using BLL.ViewModels;
using Domain;
using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class PAService : IPAService
    {
        private readonly IPARepository _repos;
        private readonly IUOW _uow;
        private readonly MyShopDb _context;
        public PAService(IPARepository repos, IUOW uow, MyShopDb db)
        {
            _repos = repos;
            _uow = uow;
            _context= db;
        }

        public int Add(PAVM model)
        {
            var entity = new ProductAttribute
            {
                Name = model.Name,
                AttributeType=model.AttributeType,
                AttributeValues = model.Values.Select(p => new ProductAttributeValue
                {
                    ProductAttributeId = p.ProductAttributeId,
                    Value = p.Value
                }).ToList()
            };
            _repos.Add(entity);
            _uow.Save();
            return entity.Id;   
        }

        public void Delete(int Id)
        {
            _repos.Delete(Id);
            _uow.Save();
        }

        public PAVM GetById(int Id)
        {
            var model = _context.ProductAttributes.GetById(Id);
            var entity = new PAVM
            {
                Id = Id,
                Name = model.Name,
                AttributeType= model.AttributeType,
                Values = model.AttributeValues.Select(ap => new PAVVM
                {
                    Id = ap.Id,
                    ProductAttributeId = ap.ProductAttributeId,
                    Value = ap.Value,
                }).ToList(),
            };
            return entity;

        }

        public List<PAVM> GetByProdId(int prodId)
        {
            var model = _context.ProductAttributes.GetByProdId(prodId);
            var newlist= new List<PAVM>();
            foreach (var item in model)
            {
                var entity = new PAVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    AttributeType=item.AttributeType,
                    Values = item.AttributeValues.Select(ap => new PAVVM
                    {
                        Id = ap.Id,
                        ProductAttributeId = ap.ProductAttributeId,
                        Value = ap.Value,
                    }).ToList(),
                };
                newlist.Add(entity);
            }
            return newlist;
        }
        public List<PAVM> GetAll()
        {
            var model = _context.ProductAttributes.GetAll();
            var newlist = new List<PAVM>();
            foreach (var item in model)
            {
                var entity = new PAVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    AttributeType= item.AttributeType,
                    Values = item.AttributeValues.Select(ap => new PAVVM
                    {
                        Id = ap.Id,
                        ProductAttributeId = ap.ProductAttributeId,
                        Value = ap.Value,
                    }).ToList(),
                };
                newlist.Add(entity);
            }
            return newlist;
        }

        public void Update(PAVM model)
        {
            var entity = _context.ProductAttributes.GetById(model.Id);
            entity.Name=model.Name;
            entity.AttributeType="a";
            entity.AttributeValues = model.Values.Select(p => new ProductAttributeValue
            {
                Id = p.Id,
                ProductAttributeId = p.ProductAttributeId,
                Value = p.Value
            }).ToList();
            _repos.Update(entity);
            _uow.Save();
        }
    }
}
