using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebKartOyunu.Controllers
{
    public class BaseController : Controller
    {
        public int getCurrentUser()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Convert.ToInt32(userID);
        }

        public string getCurrentUserClaimRole()
        {
            return User.FindFirstValue(ClaimTypes.Role);
        }

        public string getCurrentUserName()
        {
            return User.Identity.Name;
        }
    }
}
