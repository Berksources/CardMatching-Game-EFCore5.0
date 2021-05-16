using EntityLayer.UserConfig;
using System.Collections.Generic;

namespace WebKartOyunu.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IEnumerable<User> Gamers{get;set;}
        public IEnumerable<User> Admins{get;set;}
        public IEnumerable<User> AllUsers{get;set;}
        public IEnumerable<User> UserUpdate{get;set;}
    }
}
