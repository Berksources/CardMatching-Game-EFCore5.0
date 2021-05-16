using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.UserConfig
{
    public class UserRole : Base
    {
        [Key]
        public int UserRoleID { get; set; }
        public string UserRoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(userrole => userrole.UserRoleID);//Primary Key Yapılandırması

            //Data Seeding
            builder.HasData(new UserRole
            {
                UserRoleName = "Admin",
                UserRoleID = 1
            });
            builder.HasData(new UserRole
            {
                UserRoleName = "Oyuncu",
                UserRoleID = 2
            });
        }
    }
}
