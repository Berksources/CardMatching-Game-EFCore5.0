using EntityLayer;
using EntityLayer.GameCart;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryCart<T> : IRepository<Cart> 
    {
        public IEnumerable<Cart> CartsWithFileRepo();
        public List<Cart> CartsWithFileRepoByCount();
        public List<Cart> CartsWithFileRepoBlackCard();
        public IEnumerable<Cart> CartsWithFileRepoByID(int ID);
    }
    public class RepositoryCart<T> : Repository<Cart>, IRepositoryCart<T>
    {
        public RepositoryCart(KartOyunuDBContext context) : base(context) { }

        public IEnumerable<Cart> CartsWithFileRepo()
        {
            return KartOyunuDBContext.Carts.Include(x => x.FirstFileRepos).ToList();
        }

        public List<Cart> CartsWithFileRepoBlackCard()
        {
            return KartOyunuDBContext.Carts.Include(x => x.FirstFileRepos).Where(x=>x.CartName=="xx").ToList();
        }

        public List<Cart> CartsWithFileRepoByCount()
        {
            return KartOyunuDBContext.Carts.Include(x => x.FirstFileRepos).Take(5).ToList();
        }

        public IEnumerable<Cart> CartsWithFileRepoByID(int ID)
        {
            return KartOyunuDBContext.Carts.Where(x=>x.CartID==ID)
                .Include(x => x.FirstFileRepos).ToList();
        }
    }
}
