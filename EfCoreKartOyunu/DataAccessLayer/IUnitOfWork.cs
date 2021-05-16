using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using static DataAccesLayer.IRepository;

namespace DataAccesLayer
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Repository { get; }
        IRepositoryUser<T> RepositoryUser { get; }
        IRepositoryUserRole<T> RepositoryUserRole { get; }
        IRepositoryScoreTable<T> RepositoryScoreTable { get; }
        IRepositoryGameVariant<T> RepositoryGameVariant { get; }
        IRepositoryCategory<T> RepositoryCategory { get; }
        IRepositoryFirstFileRepo<T> RepositoryFirstFileRepo { get; }
        IRepositoryCart<T> RepositoryCart { get; }
        int Complete();//Save Changes
    }
}