using CORE_HBKSOFTWARE.Interfaces;
using CORE_HBKSOFTWARE.Models;
using DataAccesLayer;
using EntityLayer;
using EntityLayer.UserConfig;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebKartOyunu.Models;

namespace WebKartOyunu.Controllers
{
    public class AccountController : BaseController
    {
        readonly KartOyunuDBContext dBContext = new KartOyunuDBContext();
        private readonly IUnitOfWork<User> _unitOfWorkUser;
        private readonly IEmailSender _emailSender;
        private readonly ITokenGenerator _tokenGenerator;
        public readonly IRecaptchaValidator _recaptchaValidator;
        public readonly IPasswordHashing _passwordHashing;

        public AccountController(
            IUnitOfWork<User> unitOfWorkUser,
            IEmailSender emailSender,
            IRecaptchaValidator recaptchaValidator,
            ITokenGenerator tokenGenerator,
            IPasswordHashing passwordHashing)
        {
            _unitOfWorkUser = unitOfWorkUser;
            _emailSender = emailSender;
            _recaptchaValidator = recaptchaValidator;
            _tokenGenerator = tokenGenerator;
            _passwordHashing = passwordHashing;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(
            bool isRegistered = false,
            bool isVerifiedAccount = false,
            bool isAgainVerifiedAccount = false)
        {
            if (isRegistered)
            {
                ViewBag.Message = "Kaydınız başarılı bir şekilde alınmıştır. Hesabınızı doğrulayabilmeniz için e-mail adresinize mail gönderilmiştir.(Spam klasörünü kontrol etmeyi unutmayın.)";
                ViewBag.JS = "showControllerSuccess();";
            }
            if (isVerifiedAccount)
            {
                ViewBag.Message = "Hesabınız başarıyla doğrulanmıştır. Artık giriş yapabilirsiniz.";
                ViewBag.JS = "showControllerSuccess();";
            }
            if (isAgainVerifiedAccount)
            {
                ViewBag.Message = "Hesabınızı zaten doğrulamışsınız.";
                ViewBag.JS = "showControllerError();";
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var findUser = _unitOfWorkUser.RepositoryUser.GetUsersByEmail(loginViewModel.UserEMail);
                var userData = findUser.FirstOrDefault(x => x.UserEMail == loginViewModel.UserEMail);
                if (userData != null)
                {
                    if (userData.UserIsActive && userData.UserIsEmailConfirmed)
                    {
                        if (userData.UserRoleID == 1)
                        {
                            string ID = userData.UserID.ToString();
                            var claims = new List<Claim>
                                {
                                   new Claim(ClaimTypes.Email,userData.UserEMail),
                                   new Claim(ClaimTypes.Role,"Admin"),
                                   new Claim(ClaimTypes.NameIdentifier,ID),
                                   new Claim(ClaimTypes.Name,userData.UserName + userData.UserSurname)
                                };
                            var userIdentity = new ClaimsIdentity(claims, "UserIdentity");
                            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults
                                .AuthenticationScheme, new ClaimsPrincipal(principal),
                                new AuthenticationProperties
                                {
                                    AllowRefresh = true,
                                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                                    IsPersistent = true,
                                    IssuedUtc = DateTimeOffset.UtcNow
                                });
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (userData.UserRoleID == 2)
                        {
                            string ID = userData.UserID.ToString();
                            var claims = new List<Claim>
                                {
                                   new Claim(ClaimTypes.Email,userData.UserEMail),
                                   new Claim(ClaimTypes.Role,"Gamer"),
                                   new Claim(ClaimTypes.NameIdentifier,ID),
                                   new Claim(ClaimTypes.Name,userData.UserName + userData.UserSurname)
                                };
                            var userIdentity = new ClaimsIdentity(claims, "UserIdentity");
                            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults
                                .AuthenticationScheme, new ClaimsPrincipal(principal),
                                new AuthenticationProperties
                                {
                                    AllowRefresh = true,
                                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                                    IsPersistent = true,
                                    IssuedUtc = DateTimeOffset.UtcNow
                                });
                            return RedirectToAction("Index", "Gamer");
                        }
                        else
                        {
                            return RedirectToAction("AccessDenied", "Account");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Hesabınız aktif değil ya da doğrulanmamış! Lütfen hesabınızı doğrulayınız.";
                        ViewBag.JS = "showControllerError();";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Girmiş olduğunuz bilgilere ait bir hesap bulamadık! Lütfen tekrar deneyiniz.";
                    ViewBag.JS = "showControllerError();";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Girmiş olduğunuz verilerde hata var! Lütfen tekrar deneyiniz.";
                ViewBag.JS = "showControllerError();";
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var findUser = dBContext.Users.FirstOrDefault(
                    x => x.UserEMail == registerViewModel.User.UserEMail);
                if (findUser != null)
                {
                    ViewBag.Message = "Girmiş olduğunuz E-Mail Adresi veya Telefon Numarası ile daha önce kayıt olunmuştur! Lütfen tekrar deneyiniz.";
                    ViewBag.JS = "showControllerError();";
                    return View();
                }
                else
                {
                    if (registerViewModel.User.UserPassword != registerViewModel.UserAgainPassword)
                    {
                        ViewBag.Message = "Girmiş olduğunuz şifreler eşleşmemektedir! Lütfen tekrar deneyiniz.";
                        ViewBag.JS = "showControllerError();";
                        return View(registerViewModel);
                    }
                    else
                    {
                        var token = _tokenGenerator.GetToken(null);
                        registerViewModel.User.UserToken = token;
                        registerViewModel.User.UserIsActive = true;
                        _unitOfWorkUser.RepositoryUser.Create(registerViewModel.User);
                        _unitOfWorkUser.Complete();
                        string mailSubject = "HESABINIZI DOĞRULAYIN";
                        var verifyAccountLink =
                            Url.Action("VerifyAccount", "Account", new
                            {
                                email = registerViewModel.User.UserEMail,
                                token = token
                            }, Request.Scheme
                        );
                        var messageModel = new MessageModel(registerViewModel.User.UserName, registerViewModel.User.UserSurname, registerViewModel.User.UserEMail, mailSubject, verifyAccountLink, "verify-account", null);
                        _emailSender.EmailSendWithMimeAsync(messageModel);
                        return RedirectToAction("Login", new { isRegistered = true });
                    }
                }
            }
            else
            {
                ViewBag.Message = "Girmiş olduğunuz verilerde hata var! Lütfen tekrar deneyiniz.";
                ViewBag.JS = "showError();";
                return View(registerViewModel);
            }
        }

        [AllowAnonymous]
        public IActionResult VerifyAccount(string token, string email)
        {
            if (getCurrentUserClaimRole() != null)
            {
                if (getCurrentUserClaimRole() == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (getCurrentUserClaimRole() == "Gamer")
                {
                    return RedirectToAction("Index", "Gamer");
                }
            }
            else
            {
                if (token != null && email != null)
                {
                    var user = dBContext.Users.FirstOrDefault(x => x.UserEMail == email);
                    if (user != null)
                    {
                        if (user.UserIsEmailConfirmed != true && user.UserToken == token)
                        {
                            user.UserIsEmailConfirmed = true;
                            user.UserToken = null;
                            _unitOfWorkUser.RepositoryUser.Update(user);
                            _unitOfWorkUser.Complete();
                            return RedirectToAction("Login", new { isVerifiedAccount = true });
                        }
                        else
                        {
                            return RedirectToAction("Login", new { isAgainVerifiedAccount = true });
                        }
                    }
                    else
                    {
                        return RedirectToAction("AccessDenied", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }



        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            if (getCurrentUserClaimRole() != null)
            {
                if (getCurrentUserClaimRole() == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (getCurrentUserClaimRole() == "Gamer")
                {
                    return RedirectToAction("Index", "Gamer");
                }
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ForgotPassword(ForgotMyPasswordViewModel forgotMyPassword)
        {
            if (ModelState.IsValid)
            {
                var user = dBContext.Users.FirstOrDefault(x => x.UserEMail == forgotMyPassword.Email);
                if (user != null)
                {
                    if (user.UserIsEmailConfirmed && user.UserToken == null)
                    {
                        var token = _tokenGenerator.GetToken(null);
                        user.UserToken = token;
                        _unitOfWorkUser.RepositoryUser.Update(user);
                        _unitOfWorkUser.Complete();
                        string mailSubject = "ŞİFRE YENİLEME";
                        var passwordResetLink =
                            Url.Action("ResetPassword", "Account", new
                            {
                                email = forgotMyPassword.Email,
                                token = token
                            }, Request.Scheme
                        );
                        var messageModel = new MessageModel(user.UserName, user.UserSurname, user.UserEMail, mailSubject, passwordResetLink, "forgot-password", null);
                        _emailSender.EmailSendWithMimeAsync(messageModel);
                        TempData["Message"] = "Şifre sıfırlama bağlantınız E-mail adresinize başarıyla yollanmıştır !!";
                        TempData["JS"] = "showSuccess();";
                        return RedirectToAction("Login", new { isForgotPassword = true });
                    }
                    else
                    {
                        ViewBag.Message = "Hesabınız aktif değil ya da doğrulanmamış! Lütfen hesabınızı doğrulayınız.";
                        ViewBag.JS = "showControllerError();";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Girmiş olduğunuz e-mail adresi sistemimizde kayıtlı değil!";
                    ViewBag.JS = "showControllerError();";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Hatalı veri girişi!";
                ViewBag.JS = "showControllerError();";
                return View(forgotMyPassword);
            }
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (getCurrentUserClaimRole() != null)
            {
                if (getCurrentUserClaimRole() == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (getCurrentUserClaimRole() == "Gamer")
                {
                    return RedirectToAction("Index", "Gamer");
                }
            }
            else
            {
                if (token != null && email != null)
                {
                    var resetPasswordModel = new ResetPasswordViewModel { Token = token, Email = email };
                    return View(resetPasswordModel);
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = dBContext.Users.FirstOrDefault(
                x => x.UserEMail == resetPasswordModel.Email &&
                x.UserToken == resetPasswordModel.Token);
                if (user != null)
                {
                    if (resetPasswordModel.Password == resetPasswordModel.ConfirmPassword)
                    {
                        var hashedPassword = _passwordHashing.PasswordHash(resetPasswordModel.Password);
                        user.UserPassword = hashedPassword;
                        user.UserToken = null;
                        _unitOfWorkUser.RepositoryUser.Update(user);
                        _unitOfWorkUser.Complete();
                        return RedirectToAction("Login", new { isResettedPassword = true });
                    }
                    else
                    {
                        ViewBag.Message = "Girmiş olduğunuz şifreler eşleşmemektedir! Lütfen tekrar deneyiniz.";
                        ViewBag.JS = "showControllerError();";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Hatalı İşlem! Lütfen tekrar deneyiniz.";
                    ViewBag.JS = "showControllerError();";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Girmiş olduğunuz verilerde hata var! Lütfen tekrar deneyiniz.";
                ViewBag.JS = "showControllerError();";
                return View();
            }
        }
    }
}
