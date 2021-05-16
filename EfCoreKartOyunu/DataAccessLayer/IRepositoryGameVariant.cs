using EntityLayer;
using EntityLayer.Result;
using static DataAccesLayer.IRepository;

namespace DataAccesLayer
{
    public interface IRepositoryGameVariant<T> : IRepository<GameVariant> { }
    public class RepositoryGameVariant<T> : Repository<GameVariant>, IRepositoryGameVariant<T>
    {
        public RepositoryGameVariant(KartOyunuDBContext context) : base(context) { }
    }
}
