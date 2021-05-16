using DataAccesLayer;
using DataAccessLayer;
using EntityLayer;
using System;
using static DataAccesLayer.IRepository;

namespace BusinessLayer
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly KartOyunuDBContext _context;

        public UnitOfWork(KartOyunuDBContext context)
        {
            _context = context;
            Repository = new Repository<T>(_context);
            RepositoryUser = new RepositoryUser<T>(_context);
            RepositoryUserRole = new RepositoryUserRole<T>(_context);
            RepositoryScoreTable = new RepositoryScoreTable<T>(_context);
            RepositoryGameVariant = new RepositoryGameVariant<T>(_context);
            RepositoryCategory = new RepositoryCategory<T>(_context);
            RepositoryFirstFileRepo = new RepositoryFirstFileRepo<T>(_context);
            RepositoryCart = new RepositoryCart<T>(_context);

        }
        public IRepository<T> Repository { get; private set; }
        public IRepositoryUser<T> RepositoryUser { get; private set; }
        public IRepositoryUserRole<T> RepositoryUserRole { get; private set; }
        public IRepositoryScoreTable<T> RepositoryScoreTable { get; private set; }
        public IRepositoryGameVariant<T> RepositoryGameVariant { get; private set; }
        public IRepositoryCategory<T> RepositoryCategory { get; private set; }
        public IRepositoryCart<T> RepositoryCart { get; private set; }
        public IRepositoryFirstFileRepo<T> RepositoryFirstFileRepo { get; private set; }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return 15;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}