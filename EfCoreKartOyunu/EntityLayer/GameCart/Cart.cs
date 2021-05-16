using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.GameCart
{
    public class Cart : Base
    {
        public int CartID { get; set; }
        public int CategoryID { get; set; }
        public string CartName { get; set; }
        public Category Category { get; set; }
        public IEnumerable<FileRepo> FirstFileRepos { get; set; }
    }
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(agency => agency.CartID);//Primary Key Yapılandırması

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Carts)
                .HasForeignKey(x => x.CategoryID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
