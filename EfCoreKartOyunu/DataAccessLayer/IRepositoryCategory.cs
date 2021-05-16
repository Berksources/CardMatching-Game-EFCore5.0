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
   public interface IRepositoryCategory<T>:IRepository<Category>
    {
        public IEnumerable<Category> CategoriesWithDelete(int ID);
    }
    public class RepositoryCategory<T> : Repository<Category>, IRepositoryCategory<T>
    {
        public RepositoryCategory(KartOyunuDBContext context) : base(context) { }

        public IEnumerable<Category> CategoriesWithDelete(int ID)
        {
            return KartOyunuDBContext.Categories.Where(x => x.CategoryID == ID).ToList();
        }
    }
}
