using EntityLayer;
using EntityLayer.Result;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static DataAccesLayer.IRepository;

namespace DataAccesLayer
{
    public interface IRepositoryScoreTable<T>:IRepository<ScoreTable>
    {
        IEnumerable<ScoreTable> ScoreTablesWithUser();
        IEnumerable<ScoreTable> UserScoreResult(int ID);
    }
    public class RepositoryScoreTable<T>:Repository<ScoreTable>,IRepositoryScoreTable<T>
    {
        public RepositoryScoreTable(KartOyunuDBContext context) : base(context) { }

        public IEnumerable<ScoreTable> ScoreTablesWithUser()
        {
            return KartOyunuDBContext.ScoreTables
                .Include(x => x.User)
                .AsNoTracking().ToList();
        }

        public IEnumerable<ScoreTable> UserScoreResult(int ID)
        {
            return KartOyunuDBContext.ScoreTables.Where(x => x.UserID==ID)
                .ToList();
        }
    }
}
