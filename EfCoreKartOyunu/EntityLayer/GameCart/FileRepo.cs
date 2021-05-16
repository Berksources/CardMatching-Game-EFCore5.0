using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityLayer.GameCart
{
    public class FileRepo : Base
    {
        [Key]
        public int FileID { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FileType { get; set; }
        [Required]
        public string FileExtension { get; set; }
        public string FilePath { get; set; }
        public bool FilePhotoIsDefault { get; set; }
        public byte[] FileData { get; set; }
        public int CartID { get; set; }
        public Cart Cart { get; set; }
    }
    public class FirstFileRepoConfiguration : IEntityTypeConfiguration<FileRepo>
    {
        public void Configure(EntityTypeBuilder<FileRepo> builder)
        {
            builder.HasKey(x => x.FileID);//Primary Key Yapılandırması

            builder.HasOne(x => x.Cart)
             .WithMany(x => x.FirstFileRepos)
             .HasForeignKey(x => x.CartID)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
