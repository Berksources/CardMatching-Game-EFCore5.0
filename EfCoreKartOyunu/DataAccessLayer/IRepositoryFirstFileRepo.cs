using EntityLayer;
using EntityLayer.GameCart;
using System;
using System.Collections.Generic;
using System.Text;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryFirstFileRepo<T>:IRepository<FileRepo>{}
    public class RepositoryFirstFileRepo<T> : Repository<FileRepo>, IRepositoryFirstFileRepo<T>
    {
        public RepositoryFirstFileRepo(KartOyunuDBContext context) : base(context) { }
    }
}
