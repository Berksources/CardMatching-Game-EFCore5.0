using CORE_HBKSOFTWARE.Interfaces;
using DataAccesLayer;
using EntityLayer.GameCart;
using EntityLayer.Result;
using EntityLayer.UserConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebKartOyunu.Models;

namespace WebKartOyunu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        #region /*IUnitOfWork*/
        private readonly IUnitOfWork<User> _unitOfWorkUser;
        private readonly IUnitOfWork<UserRole> _unitOfWorkUserRole;
        private readonly IUnitOfWork<ScoreTable> _unitOfWorkScoreTable;
        private readonly IUnitOfWork<Cart> _unitOfWorkCart;
        private readonly IUnitOfWork<Category> _unitOfWorkCategory;
        private readonly IUnitOfWork<FileRepo> _unitOfWorkFirstFileRepo;
        private readonly IUnitOfWork<GameVariant> _unitOfWorkGameVariant;
        private readonly IFileUploader _fileUploader;
        #endregion

        #region /*ctor*/
        public AdminController
        (
        IUnitOfWork<User> unitOfWorkUser,
        IUnitOfWork<UserRole> unitOfWorkUserRole,
        IUnitOfWork<ScoreTable> unitOfWorkScoreTable,
        IUnitOfWork<Cart> unitOfWorkCart,
        IUnitOfWork<Category> unitOfWorkCategory,
        IUnitOfWork<FileRepo> unitOfWorkFirstFileRepo,
        IUnitOfWork<GameVariant> unitOfWorkGameVariant,
        IFileUploader fileUploader)
        {
            _unitOfWorkGameVariant = unitOfWorkGameVariant;
            _unitOfWorkFirstFileRepo = unitOfWorkFirstFileRepo;
            _unitOfWorkCategory = unitOfWorkCategory;
            _unitOfWorkCart = unitOfWorkCart;
            _unitOfWorkUser = unitOfWorkUser;
            _unitOfWorkUserRole = unitOfWorkUserRole;
            _unitOfWorkScoreTable = unitOfWorkScoreTable;
            _fileUploader = fileUploader;
        }
        #endregion
        //
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users()
        {
            var allAdmin = _unitOfWorkUser.RepositoryUser.GetAdmins();
            var allGamers = _unitOfWorkUser.RepositoryUser.GetGamers();
            var allUser = _unitOfWorkUser.RepositoryUser.GetAll();
            var userViewModel = new UserViewModel
            {
                Gamers = allGamers,
                Admins = allAdmin,
                AllUsers = allUser
            };
            return View(userViewModel);
        }

        #region /*UpdateUser*/
        [HttpPost]
        public IActionResult UpdateUsers(User user)
        {
            if (ModelState.IsValid)
            {
                user.ModifiedDate = DateTime.Now;
                _unitOfWorkUser.RepositoryUser.Update(user);
                int request = HttpContext.Response.StatusCode;
                if (request == 200)
                {
                    _unitOfWorkUser.Complete();
                    TempData["Message"] = "Kullanıcı güncelleme işleminiz başarılı!";
                    TempData["JS"] = "showSuccess();";
                    return RedirectToAction("UsersUpdateIndex", new { id = user.UserID });
                }
                else
                {
                    TempData["Message"] = "Kullanıcı güncelleme işleminiz başarısız!";
                    TempData["JS"] = "showError();";
                }
                return RedirectToAction("UsersUpdateIndex", new { id = user.UserID });
            }
            else
            {
                TempData["Message"] = "Güncellemek istediğiniz veri hatalı!";
                TempData["JS"] = "showError();";
                return RedirectToAction("UsersUpdateIndex", new { id = user.UserID });
            }
        }
        #endregion
        #region /*DeleteUser*/
        [HttpPost]
        public IActionResult DeleteUserByID(int ID)
        {
            if (ModelState.IsValid)
            {
                int request;
                _unitOfWorkUser.RepositoryUser.DeleteID(ID);
                request = HttpContext.Response.StatusCode;
                if (request == 200)
                {
                    TempData["Message"] = "Kullanıcı silme işleminiz başarılı!";
                    TempData["JS"] = "showSuccess();";
                    _unitOfWorkUser.Complete();
                }
                else
                {
                    TempData["Message"] = "Kullanıcı silme işleminiz başarısız!";
                    TempData["JS"] = "showError();";
                }
                return RedirectToAction("Users");
            }
            else
            {
                TempData["Message"] = "Silmek istediğiniz kullanıcının verisi hatalı!";
                TempData["JS"] = "showError();";
                return RedirectToAction("Users");
            }
        }
        #endregion
        public IActionResult UsersUpdateIndex(int ID)
        {
            var updateUser = _unitOfWorkUser.RepositoryUser.GetByIDForUpdate(ID);
            var userViewModel = new UserViewModel
            {
                UserUpdate = updateUser
            };
            return View(userViewModel);
        }

        #region /*DeleteScore*/
        [HttpPost]
        public IActionResult DeleteScore(int ID)
        {
            if (ModelState.IsValid)
            {
                int request;
                _unitOfWorkScoreTable.RepositoryScoreTable.DeleteID(ID);
                request = HttpContext.Response.StatusCode;
                if (request == 200)
                {
                    TempData["Message"] = "Skor silme işleminiz başarılı!";
                    TempData["JS"] = "showSuccess();";
                    _unitOfWorkScoreTable.Complete();
                }
                else
                {
                    TempData["Message"] = "Skor silme işleminiz başarısız!";
                    TempData["JS"] = "showError();";
                }
                return RedirectToAction("ScoreTable");
            }
            else
            {
                TempData["Message"] = "Silmek istediğiniz veri hatalı!";
                TempData["JS"] = "showError();";
                return RedirectToAction("ScoreTable");
            }
        }
        #endregion
        public IActionResult ScoreTable()
        {
            var allScore = _unitOfWorkScoreTable.RepositoryScoreTable.ScoreTablesWithUser();
            var scoreTableViewModel = new ScoreTableViewModel
            {
                ScoreTables = allScore
            };
            return View(scoreTableViewModel);
        }


        [HttpPost]
        public ActionResult CartIndexDeleteModal(int ID)
        {
            var deleteModal = _unitOfWorkCart.RepositoryCart.CartsWithFileRepoByID(ID);
            var cartViewModel = new CartViewModel
            {
                CartAllDataForDeleteModal = deleteModal
            };
            return PartialView(cartViewModel);
        }

        [HttpPost]
        public ActionResult CartIndexUpdateModal(int ID)
        {
            var updateModal = _unitOfWorkCart.RepositoryCart.CartsWithFileRepoByID(ID);
            var allCategory = _unitOfWorkCategory.RepositoryCategory.GetAll();
            var cartViewModel = new CartViewModel
            {
                CartAllDataForUpdateModal = updateModal,
                CategoryForDropdownList = allCategory
            };
            return PartialView(cartViewModel);
        }

        [HttpPost]
        public ActionResult CartIndexPhotoViewModal(int ID)
        {
            var photoModal = _unitOfWorkCart.RepositoryCart.CartsWithFileRepoByID(ID);
            var cartViewModel = new CartViewModel
            {
                CartAllDataForPhotoViewModal = photoModal
            };
            return PartialView(cartViewModel);
        }

        #region /*PostCart*/
        [HttpPost]
        public IActionResult PostCart(List<IFormFile> files, Cart cart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (files.Count != 0)
                    {
                        cart.CreatedDate = DateTime.Now;
                        _unitOfWorkCart.RepositoryCart.Create(cart);
                        _unitOfWorkCart.Complete();
                        var File = _fileUploader.FileUploadToDatabase(files);
                        var FileResult = File.Result;
                        FileResult.CartID = cart.CartID;
                        _unitOfWorkFirstFileRepo.RepositoryFirstFileRepo.Create(FileResult);
                        int request = HttpContext.Response.StatusCode;
                        if (request == 200)
                        {
                            _unitOfWorkFirstFileRepo.Complete();
                            TempData["Message"] = "Fotoğraf/Dosya ekleme işleminiz başarılı.";
                            TempData["JS"] = "showSuccess();";
                            return RedirectToAction("CartIndex");
                        }
                        else
                        {
                            TempData["Message"] = "Fotoğraf/Dosya ekleme işleminiz başarısız.";
                            TempData["JS"] = "showError();";
                            return RedirectToAction("CartIndex");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Fotoğraf/Dosya ekleme işleminiz başarısız.";
                        TempData["JS"] = "showError();";
                        return RedirectToAction("CartIndex");
                    }
                }
                else
                {
                    TempData["Message"] = "Eklemek istedi?inz veri hatal?!";
                    TempData["JS"] = "showError();";
                    return RedirectToAction("CartIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["JS"] = "showError();";
                return RedirectToAction("CartIndex");
            }

        }
        #endregion

        #region /*UpdateCart*/
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> UpdateCartAsync(List<IFormFile> files, int fileID, Cart cart)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (files.Count != 0)
                    {
                        cart.ModifiedDate = DateTime.Now;
                        _unitOfWorkCart.RepositoryCart.Update(cart);
                        _unitOfWorkCart.Complete();
                        var File = _fileUploader.FileUploadToDatabase(files);
                        var FileResult = File.Result;
                        FileResult.FileID = fileID;
                        FileResult.CartID = cart.CartID;
                       await _unitOfWorkFirstFileRepo.RepositoryFirstFileRepo.UpdateAsync(FileResult);
                        int request = HttpContext.Response.StatusCode;
                        if (request == 200)
                        {
                            _unitOfWorkFirstFileRepo.Complete();
                            TempData["Message"] = "Fotoğraf/Dosya ekleme işleminiz başarılı.";
                            TempData["JS"] = "showSuccess();";
                            return RedirectToAction("CartIndex");
                        }
                        else
                        {
                            TempData["Message"] = "Fotoğraf/Dosya ekleme işleminiz başarısız.";
                            TempData["JS"] = "showError();";
                            return RedirectToAction("CartIndex");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Fotoğraf/Dosya ekleme işleminiz başarısız.";
                        TempData["JS"] = "showError();";
                        return RedirectToAction("CartIndex");
                    }
                }
                else
                {
                    TempData["Message"] = "Fotoğraf/Dosya ekleme işleminiz başarısız.";
                    TempData["JS"] = "showError();";
                    return RedirectToAction("CartIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["JS"] = "showError();";
                return RedirectToAction("CartIndex");
            }
        }
        #endregion

        #region /*DeleteCart*/
        [HttpPost]
        public IActionResult DeleteCart(int CartID, Boolean FilePhotoIsDefault)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (FilePhotoIsDefault != true)//kapak fotoğrafı silinemez
                    {
                        _unitOfWorkCart.RepositoryCart.DeleteID(CartID);
                        int request = HttpContext.Response.StatusCode;
                        if (request == 200)
                        {
                            _unitOfWorkCart.Complete();
                            TempData["Message"] = "Fotoğraf/Dosya ekleme işleminiz başarısız.";
                            TempData["JS"] = "showSuccess();";
                            return RedirectToAction("CartIndex");
                        }
                        else
                        {
                            TempData["Message"] = "Fotoğraf/Dosya ekleme işleminiz başarısız.";
                            TempData["JS"] = "showError();";
                            return RedirectToAction("CartIndex");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Varsayılan fotoğrafınızı silemezsiniz. Değiştirip öyle deneyiniz.";
                        TempData["JS"] = "showError();";
                        return RedirectToAction("CartIndex");
                    }
                }
                else
                {
                    TempData["Message"] = "silmek istedi?inz veri hatal?!";
                    TempData["JS"] = "showError();";
                    return RedirectToAction("CartIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["JS"] = "showError();";
                return RedirectToAction("CartIndex");
            }
        }
        #endregion
        public IActionResult CartIndex()
        {
            var allCarts = _unitOfWorkCart.RepositoryCart.CartsWithFileRepo();
            var allCategory = _unitOfWorkCategory.RepositoryCategory.GetAll();
            var cartViewModel = new CartViewModel
            {
                CartAllData = allCarts,
                CategoryForDropdownList = allCategory
            };
            return View(cartViewModel);
        }


        [HttpPost]
        public ActionResult CategoryIndexDeleteModal(int ID)
        {
            var deleteModal = _unitOfWorkCategory.RepositoryCategory.CategoriesWithDelete(ID);
            var categoryViewModel = new CategoryViewModel
            {
                CategoriesForDeleteModal = deleteModal
            };
            return PartialView(categoryViewModel);
        }

        [HttpPost]
        public ActionResult CategoryIndexUpdateModal(int ID)
        {
            var updateModal = _unitOfWorkCategory.RepositoryCategory.CategoriesWithDelete(ID);
            var categoryViewModel = new CategoryViewModel
            {
                CategoriesForUpdateModal = updateModal
            };
            return PartialView(categoryViewModel);
        }

        #region /*PostCategory*/
        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWorkCategory.RepositoryCategory.Create(category);
                    int firstRequest = HttpContext.Response.StatusCode;
                    if (firstRequest == 200)
                    {
                        _unitOfWorkCategory.Complete();
                        TempData["Message"] = "Kategori g?ncelleme  i?leminiz ba?ar?l?";
                        TempData["JS"] = "showSuccess();";
                        return RedirectToAction("CategoryIndex");
                    }
                    else
                    {
                        TempData["Message"] = "Kategori ekleme  i?leminiz ba?ar?s?z!";
                        TempData["JS"] = "showError();";
                        return RedirectToAction("CategoryIndex");
                    }
                }
                else
                {
                    TempData["Message"] = "Eklemek istedi?iniz veri hatal?!";
                    TempData["JS"] = "showError();";
                    return RedirectToAction("CategoryIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["JS"] = "showError();";
                return RedirectToAction("CategoryIndex");
            }
        }
        #endregion
        #region /*UpdateCategory*/
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWorkCategory.RepositoryCategory.Update(category);
                    int firstRequest = HttpContext.Response.StatusCode;
                    if (firstRequest == 200)
                    {
                        _unitOfWorkCategory.Complete();
                        TempData["Message"] = "Kategori g?ncelleme  i?leminiz ba?ar?l?";
                        TempData["JS"] = "showSuccess();";
                        return RedirectToAction("CategoryIndex");
                    }
                    else
                    {
                        TempData["Message"] = "Kategori ekleme  i?leminiz ba?ar?s?z!";
                        TempData["JS"] = "showError();";
                        return RedirectToAction("CategoryIndex");
                    }
                }
                else
                {
                    TempData["Message"] = "Eklemek istedi?iniz veri hatal?!";
                    TempData["JS"] = "showError();";
                    return RedirectToAction("CategoryIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["JS"] = "showError();";
                return RedirectToAction("CategoryIndex");
            }
        }
        #endregion
        #region /*DeleteCategory*/
        [HttpPost]
        public IActionResult DeleteCategory(int CategoryID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWorkCategory.RepositoryCategory.DeleteID(CategoryID);
                    int firstRequest = HttpContext.Response.StatusCode;
                    if (firstRequest == 200)
                    {
                        _unitOfWorkCategory.Complete();
                        TempData["Message"] = "Kategori g?ncelleme  i?leminiz ba?ar?l?";
                        TempData["JS"] = "showSuccess();";
                        return RedirectToAction("CategoryIndex");
                    }
                    else
                    {
                        TempData["Message"] = "Kategori ekleme  i?leminiz ba?ar?s?z!";
                        TempData["JS"] = "showError();";
                        return RedirectToAction("CategoryIndex");
                    }
                }
                else
                {
                    TempData["Message"] = "Eklemek istedi?iniz veri hatal?!";
                    TempData["JS"] = "showError();";
                    return RedirectToAction("CategoryIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                TempData["JS"] = "showError();";
                return RedirectToAction("CategoryIndex");
            }
        }
        #endregion

        public IActionResult CategoryIndex()
        {
            var allCategory = _unitOfWorkCategory.RepositoryCategory.GetAll();
            var categoryViewModel = new CategoryViewModel
            {
                Categories = allCategory
            };
            return View(categoryViewModel);
        }
    }
}
