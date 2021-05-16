using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Result
{
    public class GameVariant : Base
    {
        [Key]
        public int GameVariantID { get; set; }
        public int GameQuestionCount { get; set; }
        public int GameScoreCount { get; set; }
        public bool GameIsOver { get; set; }
        public bool GameIsCompleted { get; set; }
    }
    public class GameVariantConfiguration : IEntityTypeConfiguration<GameVariant>
    {
        public void Configure(EntityTypeBuilder<GameVariant> builder)
        {
            builder.HasKey(x => x.GameVariantID);
            builder.HasData(new GameVariant
            {
                GameVariantID = 1,
                GameQuestionCount = 0,
                GameScoreCount = 0,
                GameIsCompleted = false,
                GameIsOver = false
            });
        }
    }
}
