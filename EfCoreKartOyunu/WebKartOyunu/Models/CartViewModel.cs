using EntityLayer.GameCart;
using System.Collections.Generic;

namespace WebKartOyunu.Models
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public IEnumerable<Cart> CartAllData { get; set; }
        public IEnumerable<Cart> CartAllDataForDeleteModal { get; set; }
        public IEnumerable<Cart> CartAllDataForUpdateModal { get; set; }
        public IEnumerable<Cart> CartAllDataForPhotoViewModal { get; set; }
        public IEnumerable<Category> CategoryForDropdownList { get; set; }
        public IEnumerable<FileRepo> FileRepo { get; set; }
    }
}
