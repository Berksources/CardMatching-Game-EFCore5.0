using EntityLayer;
using EntityLayer.UserConfig;
using static DataAccesLayer.IRepository;

namespace DataAccesLayer
{
    public interface IRepositoryUserRole<T> : IRepository<UserRole> { }
    public class RepositoryUserRole<T> : Repository<UserRole>, IRepositoryUserRole<T>
    {
        public RepositoryUserRole(KartOyunuDBContext context) : base(context) { }
    }
}
