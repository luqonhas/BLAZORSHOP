using BlazorShop.Domain.Entities;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlazorShopContext _ctx;

        public CategoryRepository(BlazorShopContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(Category category)
        {
            _ctx.Categories.Add(category);
            _ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            _ctx.Categories.Remove(SearchById(id));
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<Category> List()
        {
            return _ctx.Categories
                .AsNoTracking()
                .ToList();
        }

        public Category SearchById(Guid? id)
        {
            return _ctx.Categories.FirstOrDefault(x => x.Id == id);
        }
    }
}
