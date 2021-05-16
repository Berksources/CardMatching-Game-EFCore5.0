using EntityLayer.UserConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Result
{
   public class ScoreTable:Base
    {
        [Key]
        public int ScoreTableID { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        [Required]
        public int ScoreData { get; set; }
        public User User { get; set; }
    }
    public class ScoreTableConfiguration : IEntityTypeConfiguration<ScoreTable>
    {
        public void Configure(EntityTypeBuilder<ScoreTable> builder)
        {
            builder.HasKey(user => user.ScoreTableID);//Primary Key Yapılandırması

            /// One To Many(User-To-ScoreTables)
            builder.HasOne(cmp => cmp.User)
                .WithMany(cmpu => cmpu.ScoreTables)
                .HasForeignKey(company => company.ScoreTableID)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.HasData(new ScoreTable
            {
                ScoreTableID = 1,
                UserID = 2,
                ScoreData = 7600
            });
        }
    }
}
