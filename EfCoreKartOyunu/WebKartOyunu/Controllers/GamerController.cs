using DataAccesLayer;
using EntityLayer.GameCart;
using EntityLayer.Result;
using EntityLayer.UserConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebKartOyunu.Models;

namespace WebKartOyunu.Controllers
{
    [Authorize(Roles = "Gamer")]
    public class GamerController : BaseController
    {
        #region /*IUnitOfWork*/
        private readonly IUnitOfWork<User> _unitOfWorkUser;
        private readonly IUnitOfWork<UserRole> _unitOfWorkUserRole;
        private readonly IUnitOfWork<ScoreTable> _unitOfWorkScoreTable;
        private readonly IUnitOfWork<Cart> _unitOfWorkCart;
        private readonly IUnitOfWork<Category> _unitOfWorkCategory;
        private readonly IUnitOfWork<FileRepo> _unitOfWorkFirstFileRepo;
        private readonly IUnitOfWork<GameVariant> _unitOfWorkGameVariant;
        #endregion

        #region /*ctor*/
        public GamerController
        (
        IUnitOfWork<User> unitOfWorkUser,
        IUnitOfWork<UserRole> unitOfWorkUserRole,
        IUnitOfWork<ScoreTable> unitOfWorkScoreTable,
        IUnitOfWork<Cart> unitOfWorkCart,
        IUnitOfWork<Category> unitOfWorkCategory,
        IUnitOfWork<FileRepo> unitOfWorkFirstFileRepo,
        IUnitOfWork<GameVariant> unitOfWorkGameVariant)
        {
            _unitOfWorkGameVariant = unitOfWorkGameVariant;
            _unitOfWorkFirstFileRepo = unitOfWorkFirstFileRepo;
            _unitOfWorkCategory = unitOfWorkCategory;
            _unitOfWorkCart = unitOfWorkCart;
            _unitOfWorkUser = unitOfWorkUser;
            _unitOfWorkUserRole = unitOfWorkUserRole;
            _unitOfWorkScoreTable = unitOfWorkScoreTable;
        }
        #endregion

        public IActionResult MyScores()
        {
            var userScore = _unitOfWorkScoreTable.RepositoryScoreTable.UserScoreResult(getCurrentUser());
            var scoreTableViewModel = new ScoreTableViewModel
            {
                ScoreTableOnly = userScore
            };
            return View(scoreTableViewModel);
        }
        public IActionResult ScoreTableGamer()
        {
            var allScore = _unitOfWorkScoreTable.RepositoryScoreTable.ScoreTablesWithUser();
            var scoreTableViewModel = new ScoreTableViewModel
            {
                ScoreTables = allScore
            };
            return View(scoreTableViewModel);
        }
    }
}
