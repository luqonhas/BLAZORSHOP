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
    public class CartRepository : ICartRepository
    {
        private readonly BlazorShopContext _ctx;

        public CartRepository(BlazorShopContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(Cart cart)
        {
            _ctx.Carts.Add(cart);
            _ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            _ctx.Carts.Remove(SearchById(id));
            _ctx.SaveChanges();
        }


        // Queries:
        public IEnumerable<Cart> List()
        {
            return _ctx.Carts
                .AsNoTracking()
                //.Include(x => x.ItemsCart)
                .ToList();
        }

        public Cart SearchById(Guid? id)
        {
            return _ctx.Carts.FirstOrDefault(x => x.Id == id);
        }

        public Cart SearchByUserId(Guid? id)
        {
            return _ctx.Carts.FirstOrDefault(x => x.IdUser == id);
        }
    }
}
