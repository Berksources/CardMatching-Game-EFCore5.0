using EntityLayer;
using EntityLayer.UserConfig;
using System.Collections.Generic;
using System.Linq;
using static DataAccesLayer.IRepository;

namespace DataAccesLayer
{
    public interface IRepositoryUser<T> : IRepository<User>
    {
        IEnumerable<User> GetUsersByEmail(string userEmail);
        IEnumerable<User> GetGamers();
        IEnumerable<User> GetAdmins();
        IEnumerable<User> GetByIDForUpdate(int ID);
    }
    public class RepositoryUser<T> : Repository<User>, IRepositoryUser<T>
    {
        public RepositoryUser(KartOyunuDBContext context) : base(context) { }

        public IEnumerable<User> GetUsersByEmail(string userEmail)
        {
            return KartOyunuDBContext.Users.Where(x => x.UserEMail == userEmail).ToList();
        }
        public IEnumerable<User> GetGamers()
        {
            return KartOyunuDBContext.Users.Where(x => x.UserRoleID == 2).ToList();
        }
        public IEnumerable<User> GetAdmins()
        {
            return KartOyunuDBContext.Users.Where(x => x.UserRoleID == 1).ToList();
        }

        public IEnumerable<User> GetByIDForUpdate(int ID)
        {
            return KartOyunuDBContext.Users.Where(x => x.UserID == ID).ToList();
        }
    }
}
