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
    public class ItemCartRepository : IItemCartRepository
    {
        private readonly BlazorShopContext _ctx;

        public ItemCartRepository(BlazorShopContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(ItemCart item)
        {
            _ctx.ItemsCart.Add(item);
            _ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            _ctx.ItemsCart.Remove(SearchById(id));
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<ItemCart> List()
        {
            return _ctx.ItemsCart
                .AsNoTracking()
                .ToList();
        }

        public ItemCart SearchById(Guid? id)
        {
            return _ctx.ItemsCart.FirstOrDefault(x => x.Id == id);
        }
    }
}
