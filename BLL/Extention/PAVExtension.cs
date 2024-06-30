using BLL.Service;
using BLL.ViewModels;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Extention
{
    public static class PAVExtension
    {

     

        public static async Task<List<Product>> Filter(this DbSet<ProductAttributeValue> db, FilterVM model)
        {
            /////////////sax toxhniya meak pavs chi berum
            var a = await db.Include(p=>p.ProductAttribute).Include(p => p.Products).ThenInclude(p => p.Category).GroupBy(p => p.ProductAttribute.Name).ToListAsync();
            var newlist = new List<Product>();
            foreach (var item in a)
            {
                if (item.Key == nameof(model.ScreenSize) && model.ScreenSize != null)
                {
                    foreach (var item1 in item)
                    {
                        var b = model.ScreenSize.Contains(item1.Value) ? item1.Products.ToList() : null;
                        if (b == null)
                        {
                            continue;
                        }

                        newlist.AddRange(b.Except(newlist, new ProductEqualityComparer()));
                    }
                    model.ScreenSize = null;
                }
                if (item.Key == nameof(model.RAM) && model.RAM != null)
                {

                    foreach (var item1 in item)
                    {
                        var b = model.RAM.Contains(item1.Value) ? item1.Products.ToList() : null;
                        if (b == null)
                        {
                            continue;
                        }
                        newlist.AddRange(b.Except(newlist, new ProductEqualityComparer()));
                    }
                    model.RAM = null;
                }
                if (item.Key == nameof(model.Memory) && model.Memory != null)
                {
                    foreach (var item1 in item)
                    {
                        var b = model.Memory.Contains(item1.Value) ? item1.Products.ToList() : null;
                        if (b == null)
                        {
                            continue;
                        }
                        newlist.AddRange(b.Except(newlist, new ProductEqualityComparer()));
                    }
                    model.Memory = null;
                }

            }
            return newlist;
        }
        public static List<ProductAttributeValue> GetAll(this DbSet<ProductAttributeValue> db, List<int> list, int prodId)
        {
            var sortedlist = new List<ProductAttributeValue>();
            var values = db.ToList();
            foreach (var item in values)
            {
                if (list.Contains(item.Id))
                {
                    list.Remove(item.Id);
                    //item.ProductId=prodId;
                    sortedlist.Add(item);
                    continue;
                }
            }
            return sortedlist;
        }
    }
}
