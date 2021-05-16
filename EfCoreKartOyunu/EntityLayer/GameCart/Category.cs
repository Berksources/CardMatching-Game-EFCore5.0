using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.GameCart
{
    public class Category : Base
    {
        [Key]
        public int CategoryID { get; set; }
        [Required, StringLength(50)]
        public string CategoryName { get; set; }
        public bool CategoryIsActive { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
    }
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(category => category.CategoryID);//Primary Key Yapılandırması

            
        }
    }
}
