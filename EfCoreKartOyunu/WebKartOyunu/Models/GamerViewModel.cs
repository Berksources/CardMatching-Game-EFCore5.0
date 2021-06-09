using EntityLayer.GameCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebKartOyunu.Models
{
    public class GamerViewModel
    {
       public List<Cart> Carts { get; set; }
       public List<Cart> BlackCart { get; set; }
    }
}
