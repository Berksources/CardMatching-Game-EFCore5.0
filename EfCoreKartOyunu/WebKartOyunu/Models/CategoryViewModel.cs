using EntityLayer.GameCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebKartOyunu.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Category> CategoriesForDeleteModal { get; set; }
        public IEnumerable<Category> CategoriesForUpdateModal { get; set; }
    }
}
