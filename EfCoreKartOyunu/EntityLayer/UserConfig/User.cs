using EntityLayer.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.UserConfig
{
    public class User:Base
    {
        [Key]
        public int UserID { get; set; }
        public int UserRoleID { get; set; }
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        public string UserSurname { get; set; }
        [Required, MinLength(8, ErrorMessage = "Şifreniz 8 karakterden az olamaz.")]
        public string UserPassword { get; set; }
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string UserEMail { get; set; }
        public string UserLastScore { get; set; }
        public bool UserIsActive { get; set; }
        public bool UserIsEmailConfirmed { get; set; }//UserIsConfirmed
        public string UserToken { get; set; }

        /// <summary>//FluentApi
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<ScoreTable> ScoreTables { get; set; }
        /// </summary>//FluentApi
        
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.UserID);//Primary Key Yapılandırması

            /// One To Many(UserRole - To - User)
            builder.HasOne(x => x.UserRole)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserRoleID)//Foreign Key
                .OnDelete(DeleteBehavior.NoAction)//Silinerken ilişkili verileri silme özelliğini kapattık
                .IsRequired(false);

            //Data Seeding
            builder.HasData(new User
            {//Admin
                UserID = 1,
                UserName = "Admin",
                UserSurname = "Admin",
                UserPassword = "AdminGitHub",
                UserEMail = "AdminPaneli@gmail.com",
                UserIsActive = true,
                UserIsEmailConfirmed = true,
                CreatedDate = DateTime.Now,
                UserRoleID = 1
            });
            builder.HasData(new User
            {//Oyuncu
                UserID = 2,
                UserName = "Kullanici",
                UserSurname = "Kullanici",
                UserPassword = "KullaniciGitHub",
                UserEMail = "Kullanici@gmail.com",
                UserIsActive = true,
                UserIsEmailConfirmed = true,
                CreatedDate = DateTime.Now,
                UserRoleID = 2
            });
        }
    }
}
